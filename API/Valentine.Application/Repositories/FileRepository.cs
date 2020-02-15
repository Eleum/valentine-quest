using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Application.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IValentineDbContext _dbContext;

        public FileRepository(IValentineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddSingleFile(File file)
        {
            _dbContext.Images.Add((Image)file);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AddFiles<T>(IEnumerable<T> files) where T: File
        {
            try
            {
                foreach (var file in files)
                {
                    _dbContext.Images.Add(file as Image);
                }
            }
            catch (Exception e)
            {

            }
            

            return await _dbContext.SaveChangesAsync();
        }
    }
}
