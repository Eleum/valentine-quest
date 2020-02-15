using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Contracts.Requests
{
    public class FilesUploadRequest
    {
        public IFormFile[] Files { get; set; }
        
        public string[] Ids { get; set; }
    }
}
