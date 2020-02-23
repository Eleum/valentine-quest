using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IMapsRepository
    {
        Task<Tuple<Guid, IEnumerable<Map>>> GetMapsByAppKey(string appKey);

        Task<int> SaveMap(Map map);
    }
}
