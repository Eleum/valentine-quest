using System;

namespace Valentine.Shared.Contracts.Requests
{
    public class MapSaveRequest
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }
    }
}
