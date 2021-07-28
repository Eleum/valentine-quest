using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Valentine.Shared.Contracts.Requests;
using Valentine.Shared.Contracts.Responses;
using Valentine.Application.Interfaces;
using Valentine.Domain;
using Valentine.Shared.Contracts.Models;

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

        [HttpGet]
        public async Task<IActionResult> GetAreas([FromQuery]AreasRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MapId))
                return BadRequest();

            var areasData = await _areasRepository.GetAreas(Guid.Parse(request.MapId));
            var areas = areasData.Select(x => new AreaModel
            {
                Id = x.Id,
                Progress = x.Progress,
                GeoPoints = x.GeoPoints.Select(x => new GeoPointModel(x.AreaId.ToString(), x.Index, x.Latitude, x.Longitude))
            });

            return Ok(JsonSerializer.Serialize(new AreasResponse { Areas = areas }));
        }

        [HttpPost]
        public async Task<IActionResult> SaveAreas([FromBody]AreasSaveRequest request)
        {
            var areas = request.GeoPoints
                .GroupBy(x => x.AreaId)
                .Select(x => x.First())
                .Select(x => new Area(Guid.Parse(x.AreaId), Guid.Parse(request.MapId)));

            var geoPoints = request.GeoPoints.Select(x => new GeoPoint(Guid.Parse(x.AreaId), x.Index, x.Latitude, x.Longitude));

            await _areasRepository.SaveAreasAsync(areas);
            await _geoPointsRepository.SaveAreaGeoPointsAsync(geoPoints);

            return Ok();
        }
    }
}
