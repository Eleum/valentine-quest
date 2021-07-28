using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class GeoPoint
    {
        public Guid Id { get; init; }

        public Guid AreaId { get; set; }

        public Area Area { get; set; }

        public int Index { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public GeoPoint(Guid areaId, int index, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            AreaId = areaId;
            Index = index;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
