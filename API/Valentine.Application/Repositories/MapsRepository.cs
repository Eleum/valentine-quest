using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class MapsRepository : IMapsRepository
    {
        private readonly IValentineDbContext _dbContext;

        public MapsRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Map>> GetUserMapsAsync(Guid userId)
        {
            return await _dbContext.Maps.Where(x => x.UserId == userId)
                .Include(m => m.Areas)
                .ThenInclude(a => a.Files)
                .ToListAsync();
        }

        public async Task<int> SaveMapAsync(Map map)
        {
            _dbContext.Maps.Add(map);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
