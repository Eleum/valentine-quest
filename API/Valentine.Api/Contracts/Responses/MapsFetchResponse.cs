using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Contracts.Responses
{
    public class MapsFetchResponse
    {
        public Guid UserId { get; set; }

        public IEnumerable<MapsCollectionItem> Maps { get; set; }
    }

    public class MapsCollectionItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public double OverallProgress { get; set; }

        public MapsCollectionItem(Guid id, string title, string description, DateTimeOffset createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
