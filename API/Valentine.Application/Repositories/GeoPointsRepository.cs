using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class GeoPointsRepository : IGeoPointsRepository
    {
        public Task<GeoPoint> GetAreaGeoPoints(Guid areaId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAreaGeoPoints(IEnumerable<GeoPoint> geoPoints)
        {
            throw new NotImplementedException();
        }
    }
}
