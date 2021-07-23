using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Shared.Contracts.Models;

namespace Valentine.Shared.Contracts.Responses
{
    public class AreasResponse
    {
        public IEnumerable<AreaModel> Areas { get; set; }
    }
}
