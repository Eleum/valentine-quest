using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valentine.Domain
{
    public class Area
    {
        public Guid Id { get; set; }

        public Guid MapId { get; set; }

        public Map Map { get; set; }

        public ICollection<GeoPoint> GeoPoints { get; set; }

        public ICollection<File> Files { get; set; }

        public double Progress => (Files?.Count ?? 0) == 0 ? 0 : (Files.Count > 5 ? 5 : Files.Count) / 5D * 100;

        public Area(Guid id, Guid mapId)
        {
            Id = id;
            MapId = mapId;
        }
    }
}
