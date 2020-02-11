using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Interfaces
{
    public interface IFileCloudUploader
    {
        Task<string> Upload(byte[] fileBytes, string fileName, string contentType);
    }
}
