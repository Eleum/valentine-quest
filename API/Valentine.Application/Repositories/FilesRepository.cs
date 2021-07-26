using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IValentineDbContext _dbContext;

        public FilesRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddFiles<T>(IEnumerable<T> files) where T : File
        {
            foreach (var file in files)
            {
                _dbContext.Set<T>().Add(file);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
