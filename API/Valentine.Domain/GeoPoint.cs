using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class GeoPoint
    {
        public Guid Id { get; set; }

        public Guid AreaId { get; set; }

        public int Position { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public GeoPoint(Guid areaId, int position, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            AreaId = areaId;
            Position = position;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
