using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Shared.Contracts.Requests;
using Valentine.Shared.Contracts.Responses;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapsRepository _mapsRepository;

        public MapsController(IUsersRepository usersRepository, IMapsRepository mapsRepository)
        {
            _usersRepository = usersRepository;
            _mapsRepository = mapsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetExistingMaps([FromQuery]MapsFetchRequest request)
        {
            if (request.AppKey is null) return BadRequest();

            //TODO: shit api design, refactor this
            User user;

            try
            {
                user = _usersRepository.GetUserWithAppKey(request.AppKey);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(JsonConvert.SerializeObject(e.Message));
            }

            var userMapsCollection = await _mapsRepository.GetUserMapsAsync(user.Id);
            var response = new MapsFetchResponse
            {
                UserId = user.Id,
                Maps = userMapsCollection.Select(x => new MapsCollectionItem(x.Id, x.Title, x.Description, x.CreatedAt) 
                { 
                    OverallProgress = (x.Areas?.Count ?? 0) == 0 ? -1D : x.Areas.Sum(a => a.Progress) / x.Areas.Count * 100
                })
            };

            //TODO: global serializer settings
            return Ok(JsonConvert.SerializeObject(response, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            }));
        }

        [HttpPost]
        public async Task<IActionResult> SaveMap([FromBody]MapSaveRequest request)
        {
            var map = new Map
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(request.UserId),
                Title = request.Title,
                Description = request.Description,
                IsDefault = request.IsDefault,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _mapsRepository.SaveMapAsync(map);
            return CreatedAtAction(nameof(GetExistingMaps), map);
        }
    }
}
