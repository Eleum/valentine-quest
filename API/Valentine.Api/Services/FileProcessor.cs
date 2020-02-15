using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Interfaces;

namespace Valentine.Api.Services
{
    public class FileProcessor : IFileProcessor
    {
        public byte[] ConvertFileToByteArray(IFormFile image)
        {
            byte[] result = null;

            using (var fileStream = image.OpenReadStream())
            {
                using var stream = new MemoryStream();
                fileStream.CopyTo(stream);
                result = stream.ToArray();
            }

            return result;
        }
    }
}
