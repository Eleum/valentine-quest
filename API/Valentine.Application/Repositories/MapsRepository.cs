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

        public async Task<IEnumerable<Map>> GetMapsByAppKey(string appKey)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.AppKey == appKey);

            return user != null ? await _dbContext.Maps.Where(x => x.UserId == user.Id).Include(a => a.Areas).ToListAsync() : null;
        }
    }
}
