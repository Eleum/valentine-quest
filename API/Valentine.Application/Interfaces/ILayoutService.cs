using System.Collections.Generic;
using Valentine.Application.Models;
using Valentine.Shared.Contracts.Responses;

namespace Valentine.Application.Interfaces
{
    public interface ILayoutService
    {
        FeatureCollection<Polygon> InitializeMapAreas(IEnumerable<AreasCollectionItem> rawAreas);
    }
}