using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Enums;
using Valentine.Application.Interfaces;
using Valentine.Application.Models;
using Valentine.Shared.Contracts.Responses;

namespace Valentine.Application.Services
{
    public class LayoutService : ILayoutService
    {
        public FeatureCollection<Polygon> InitializeMapAreas(IEnumerable<AreasCollectionItem> rawAreas)
        {
            var areas = CreateAreas(rawAreas);
            var polygons = CreatePolygons(areas);

            return new FeatureCollection<Polygon>
            {
                Type = FeatureType.FeatureCollection,
                Features = polygons
            };
        }

        private IEnumerable<Area> CreateAreas(IEnumerable<AreasCollectionItem> rawAreas)
        {
            return rawAreas.Select(x => new Area
            {
                Id = x.Id.ToString(),
                Points = x.GeoPoints.OrderByDescending(p => p.Position).Select(p => new[] { p.Latitude, p.Longitude })
            });
        }
        
        private IEnumerable<Polygon> CreatePolygons(IEnumerable<Area> areas)
        {
            return areas.Select(x => new Polygon
            {
                Type = FeatureType.Feature,
                Geometry = new Geometry(FeatureType.Polygon, x.Points),
                Properties = new Dictionary<string, object>
                {
                    { "id", x.Id },
                    { "completion", new Random().Next(0, 101) }
                }
            });
        }
    }
}
