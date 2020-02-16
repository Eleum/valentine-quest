using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IAreasRepository
    {
        Task<IEnumerable<Guid>> GetAreas(Guid mapId);

        Task<int> SaveAreasAsync(IEnumerable<Area> areas);
    }
}
