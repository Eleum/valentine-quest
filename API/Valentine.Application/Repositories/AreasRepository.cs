using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;

namespace Valentine.Application.Repositories
{
    public class AreasRepository : IAreasRepository
    {
        private readonly IValentineDbContext _dbContext;

        public AreasRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Guid>> GetAreas(Guid mapId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAreasAsync(Guid mapId, IEnumerable<Guid> areasIds)
        {
            foreach (var areaId in areasIds)
            {
                _dbContext.Areas.Add(new Domain.Area(areaId, mapId));
            }

            throw new NotImplementedException();
        }
    }
}
