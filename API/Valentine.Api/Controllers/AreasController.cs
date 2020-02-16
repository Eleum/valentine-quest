using Microsoft.AspNetCore.Mvc;
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
    public class AreasController : ControllerBase
    {
        private readonly IAreasRepository _areasRepository;
        private readonly IGeoPointsRepository _geoPointsRepository;

        public AreasController(IAreasRepository areasRepository, IGeoPointsRepository geoPointsRepository)
        {
            _areasRepository = areasRepository;
            _geoPointsRepository = geoPointsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAreas([FromBody]AreasSaveRequest request)
        {
            // TODO:
            return Ok();
        }
    }
}
