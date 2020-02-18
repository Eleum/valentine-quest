using System.Collections.Generic;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IMapsRepository
    {
        Task<IEnumerable<Map>> GetMapsByAppKey(string appKey);
    }
}
