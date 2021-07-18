using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
using Valentine.Api.Contracts.Responses;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly IMapsRepository _mapsRepository;

        public MapsController(IMapsRepository mapsRepository)
        {
            _mapsRepository = mapsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetExistingMaps([FromQuery]MapsFetchRequest request)
        {
            if (request.AppKey is null) return BadRequest();

            var userMapsCollection = await _mapsRepository.GetMapsByAppKey(request.AppKey);
            if (userMapsCollection is null) return BadRequest();

            var maps = new MapsFetchResponse
            {
                UserId = userMapsCollection.Value.Key,
                Maps = userMapsCollection.Value.Value
                    .Select(x => new MapsCollectionItem(x.Id, x.Title, x.Description, x.CreatedAt) 
                    { 
                        OverallProgress = x.Areas.Sum(a => a.Progress) / x.Areas.Count * 100
                    })
            };

            return Ok(JsonConvert.SerializeObject(maps, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(),     
                Formatting = Formatting.Indented 
            }));
        }

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

            await _mapsRepository.SaveMap(map);
            return CreatedAtAction(nameof(GetExistingMaps), map);
        }
    }
}
