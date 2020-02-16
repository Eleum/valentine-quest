using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IGeoPointsRepository
    {
        Task<GeoPoint> GetAreaGeoPoints(Guid areaId);

        Task<int> SaveAreaGeoPoints(IEnumerable<GeoPoint> geoPoints);
    }
}
