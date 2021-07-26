using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Valentine.Shared.Contracts.Requests
{
    public class StoryUploadRequest
    {
        public IEnumerable<IFormFile> Files { get; set; }

        public string AreaId { get; set; }
    }
}
