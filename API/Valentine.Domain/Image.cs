using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class Image : File
    {
        public string Url { get; set; }

        public Image(Guid id, string url, Guid areaId)
        {
            Id = id;
            Url = url;
            AreaId = areaId;
        }
    }
}
