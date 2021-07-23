using System.Collections.Generic;
using Valentine.Shared.Contracts.Models;

namespace Valentine.Shared.Contracts.Requests
{
    public class AreasSaveRequest
    {
        public string MapId { get; set; }

        public IEnumerable<GeoPointModel> GeoPoints { get; set; }
    }    
}
