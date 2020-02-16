using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.Application.Interfaces
{
    public interface IAreasRepository
    {
        Task<IEnumerable<Guid>> GetAreas(Guid mapId);

        Task<int> SaveAreasAsync(Guid mapId, IEnumerable<Guid> areasIds);
    }
}
