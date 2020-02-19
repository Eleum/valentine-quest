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
            if (request.AppKey == null)
                return BadRequest();

            var maps = new MapsFetchResponse
            {
                Maps = (await _mapsRepository.GetMapsByAppKey(request.AppKey))
                    .Select(x => new MapsCollectionItem(x.Id, x.Title, x.Description, x.CreatedAt) 
                    { 
                        OverallProgress = x.Areas.Sum(a => a.Progress) / x.Areas.Count * 100
                    })
            };
            return maps.Maps == null 
                ? (IActionResult)BadRequest() 
                : Ok(JsonConvert.SerializeObject(maps, new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
    }
}
