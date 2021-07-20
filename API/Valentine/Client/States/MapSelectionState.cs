using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Client.States
{
    public class MapSelectionState
    {
        public delegate Task AsyncTaskHandler();
        public event AsyncTaskHandler OnNewMapCreatedAsync;

        public async Task RegisterNewMapCreatedAsync()
        {
            await OnNewMapCreatedAsync.Invoke();
        }
    }
}
