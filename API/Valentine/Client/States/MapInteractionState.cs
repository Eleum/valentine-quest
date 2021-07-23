using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Application.Models;
using Valentine.Shared.Contracts.Models;
using Valentine.Shared.Contracts.Responses;

namespace Valentine.Client.States
{
    public class MapInteractionState
    {
        public delegate Task AsyncTaskHandler();
        public delegate Task AsyncTaskHandler<T>(T param);

        public event AsyncTaskHandler OnNewMapCreatedAsync;
        public event AsyncTaskHandler<MapModel> OnMapSelectedAsync;
        public event AsyncTaskHandler<AreaModel> OnAreaSelectedAsync;

        public async Task RegisterNewMapCreatedAsync()
        {
            await OnNewMapCreatedAsync.Invoke();
        }
        
        public async Task HandleMapSelectionAsync(MapModel map)
        {
            await OnMapSelectedAsync.Invoke(map);
        }
    }
}
