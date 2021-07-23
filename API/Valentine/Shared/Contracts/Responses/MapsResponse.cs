using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Shared.Contracts.Models;

namespace Valentine.Shared.Contracts.Responses
{
    public class MapsResponse
    {
        public Guid UserId { get; set; }

        //TODO: create separate collection
        public IEnumerable<MapModel> Maps { get; set; }
    }
}
