using System.Collections.Generic;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class GeoPointsRepository : IGeoPointsRepository
    {
        private readonly IValentineDbContext _dbContext;

        public GeoPointsRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAreaGeoPoints(IEnumerable<GeoPoint> geoPoints)
        {
            foreach (var geoPoint in geoPoints)
            {
                _dbContext.GeoPoints.Add(geoPoint);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
