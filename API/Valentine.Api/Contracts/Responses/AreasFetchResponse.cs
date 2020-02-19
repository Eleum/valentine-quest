using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Contracts.Responses
{
    public class AreasFetchResponse
    {
        public IEnumerable<AreasCollectionItem> Areas { get; set; }
    }

    public class AreasCollectionItem
    {
        public Guid Id { get; set; }

        public IEnumerable<AreaGeoPoint> GeoPoints { get; set; }
    }

    public class AreaGeoPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Position { get; set; }

        public AreaGeoPoint(double latitude, double longitude, int position)
        {
            Latitude = latitude;
            Longitude = longitude;
            Position = position;
        }
    }
}
