using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IFilesRepository
    {
        Task<int> AddSingleFile(File file);

        Task<int> AddFiles<T>(IEnumerable<T> files) where T : File;
    }
}
