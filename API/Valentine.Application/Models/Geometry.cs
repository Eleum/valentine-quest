using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Valentine.Application.Enums;

namespace Valentine.Application.Models
{
    public class Geometry : Feature
    { 
        public IEnumerable<GeoPointCoordsCollection> Coordinates { get; set; }

        public Geometry(FeatureType type, IEnumerable<GeoPointCoordsCollection> coordinates)
        {
            Type = type;
            Coordinates = coordinates;
        }
    }
}
