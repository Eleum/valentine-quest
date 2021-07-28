using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class AreasRepository : IAreasRepository
    {
        private readonly IValentineDbContext _dbContext;

        public AreasRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Area>> GetAreas(Guid mapId)
        {
            return await _dbContext.Areas
                .Where(x => x.MapId == mapId)
                .Include(a => a.GeoPoints)
                .Include(a => a.Files)
                .ToListAsync();
        }

        public async Task<int> SaveAreasAsync(IEnumerable<Area> areas)
        {
            foreach (var area in areas)
            {
                _dbContext.Areas.Add(area);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
