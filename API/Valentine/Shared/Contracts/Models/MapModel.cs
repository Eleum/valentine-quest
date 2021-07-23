using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.Shared.Contracts.Models
{
    public class MapModel
    {
        public string Id { get; init; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public double OverallProgress { get; init; }

        public MapModel(string id, string title, string description, DateTimeOffset createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
