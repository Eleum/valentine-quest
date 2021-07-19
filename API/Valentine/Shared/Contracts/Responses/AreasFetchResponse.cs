﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Shared.Contracts.Responses
{
    public class AreasFetchResponse
    {
        public IEnumerable<AreasCollectionItem> Areas { get; set; }
    }

    public class AreasCollectionItem
    {
        public Guid Id { get; set; }

        public IEnumerable<GeoPoint> GeoPoints { get; set; }
    }

    public class GeoPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Position { get; set; }

        public GeoPoint(double latitude, double longitude, int position)
        {
            Latitude = latitude;
            Longitude = longitude;
            Position = position;
        }
    }
}
