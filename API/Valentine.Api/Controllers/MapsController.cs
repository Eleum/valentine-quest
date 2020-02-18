using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
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

            var maps = (await _mapsRepository.GetMapsByAppKey(request.AppKey))?.Select(x => x.Id);
            return maps == null ? (IActionResult)BadRequest() : Ok(JsonConvert.SerializeObject(maps));
        }
    }
}
