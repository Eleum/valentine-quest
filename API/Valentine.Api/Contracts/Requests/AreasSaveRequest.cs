using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Contracts.Requests
{
    public class AreasSaveRequest
    {
        public List<Guid> Ids { get; set; }
    }
}
