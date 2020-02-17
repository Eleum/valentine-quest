using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
using Valentine.Application.Interfaces;
using Valentine.Domain;

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
            var areas = request.Data
                .Select(x => x.AreaId)
                .Distinct()
                .Select(x => new Area(Guid.Parse(x), Guid.Parse("5C6949EB-B7CB-4C39-8F6A-B989A4936B58")));

            var geoPoints = request.Data.Select(x => new GeoPoint(Guid.Parse(x.AreaId), x.Position, x.Latitude, x.Longitude));

            await _areasRepository.SaveAreasAsync(areas);
            await _geoPointsRepository.SaveAreaGeoPoints(geoPoints);
            return Ok(); 
        }
    }
}
