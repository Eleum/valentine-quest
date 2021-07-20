using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IValentineDbContext _dbContext;

        public UsersRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserWithAppKey(string appKey)
        {
            return _dbContext.Users.Single(x => x.AppKey == appKey);
        }
    }
}
