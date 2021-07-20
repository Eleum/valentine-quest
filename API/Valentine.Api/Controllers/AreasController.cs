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
        public async Task<IActionResult> GetAreas([FromQuery]AreasFetchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MapId))
                return BadRequest();

            var areas = await _areasRepository.GetAreas(Guid.Parse(request.MapId));
            var responseAreas = areas.Select(x => new AreasCollectionItem
            {
                Id = x.Id,
                GeoPoints = areas.Where(a => a.Id == x.Id).SelectMany(x => x.GeoPoints).Select(x => new GeoPointItem(x.Latitude, x.Longitude, x.Position))
            });

            var response = new AreasFetchResponse { Areas = responseAreas };
            return Ok(JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }

        [HttpPost]
        public async Task<IActionResult> SaveAreas([FromBody]AreasSaveRequest request)
        {
            //TODO: deal with mapid for areas
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
