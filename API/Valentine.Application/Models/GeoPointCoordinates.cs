using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Valentine.Application.Converters;

namespace Valentine.Application.Models
{
    public class GeoPointCoordinates
    {
        public double[] Coordinates { get; init; }

        public GeoPointCoordinates(double latitude, double longitude)
        {
            Coordinates = new[] { latitude, longitude };
        }
        
        public GeoPointCoordinates(double[] coordinates)
        {
            Coordinates = coordinates;
        }
    }

    [JsonConverter(typeof(GeoPointCoordinatesCollectionJsonConverter))]
    public class GeoPointCoordsCollection : IEnumerable<GeoPointCoordinates>
    {
        public IEnumerable<GeoPointCoordinates> Coordinates { get; }

        public GeoPointCoordsCollection(IEnumerable<GeoPointCoordinates> coordinates)
        {
            Coordinates = coordinates;
        }

        public IEnumerator<GeoPointCoordinates> GetEnumerator()
        {
            return Coordinates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Coordinates.GetEnumerator();
        }
    }
}
