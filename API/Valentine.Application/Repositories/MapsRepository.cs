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

        public async Task<KeyValuePair<Guid, IEnumerable<Map>>?> GetMapsByAppKey(string appKey)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.AppKey == appKey);
            return user is null 
                ? (KeyValuePair<Guid, IEnumerable<Map>>?)null 
                : new KeyValuePair<Guid, IEnumerable<Map>>(
                    user.Id, 
                    await _dbContext.Maps.Where(x => x.UserId == user.Id).Include(a => a.Areas).ToListAsync());
        }

        public async Task<int> SaveMap(Map map)
        {
            var user = _dbContext.Users.FirstOrDefaultAsync(x => x.Id == map.UserId);
            if (user is null) throw new ArgumentException("The user id is not valid");

            _dbContext.Maps.Add(map);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
