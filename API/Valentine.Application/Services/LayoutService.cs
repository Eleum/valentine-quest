using System.Collections.Generic;
using System.Linq;
using Valentine.Application.Enums;
using Valentine.Application.Helpers;
using Valentine.Application.Interfaces;
using Valentine.Application.Models;
using Valentine.Shared.Contracts.Models;

namespace Valentine.Application.Services
{
    public class LayoutService : ILayoutService
    {
        public FeatureCollection<Polygon> InitializeMapAreas(IEnumerable<AreaModel> areasData)
        {
            var areas = CreateAreas(areasData);
            var polygons = CreatePolygons(areas);

            return new FeatureCollection<Polygon>
            {
                Type = FeatureType.FeatureCollection,
                Features = polygons
            };
        }

        private IEnumerable<Area> CreateAreas(IEnumerable<AreaModel> areasData)
        {
            return areasData.Select(x => new Area
            {
                Id = x.Id.ToString(),
                Progress = x.Progress,
                Points = new GeoPointCoordsCollection(x.GeoPoints
                    .OrderByDescending(p => p.Index)
                    .Select(p => new GeoPointCoordinates(p.Latitude, p.Longitude)))
            });
        }
        
        private IEnumerable<Polygon> CreatePolygons(IEnumerable<Area> areas)
        {
            return areas.Select(x => new Polygon
            {
                Type = FeatureType.Feature,
                Geometry = new Geometry(FeatureType.Polygon, Enumerable.Repeat(new GeoPointCoordsCollection(x.Points), 1)),
                Properties = new Dictionary<string, object>
                {
                    { Constants.Properties.ID, x.Id },
                    { Constants.Properties.COMPLETION, x.Progress }
                }
            });
        }
    }
}
