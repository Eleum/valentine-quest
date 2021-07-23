using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.Shared.Contracts.Models
{
    public class GeoPointModel
    {
        public string AreaId { get; set; }

        public int Index { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public GeoPointModel(string areaId, int index, double latitude, double longitude)
        {
            AreaId = areaId;
            Index = index;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
