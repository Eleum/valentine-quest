using System;
using System.Collections.Generic;

namespace Valentine.Domain
{
    public class Map
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public List<Area> Areas { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}
