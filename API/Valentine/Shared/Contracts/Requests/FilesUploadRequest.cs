using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Shared.Contracts.Requests
{
    public class FilesUploadRequest
    {
        public object[] Files { get; set; }
        
        public string[] Ids { get; set; }

        public string AreaId { get; set; }
    }
}
