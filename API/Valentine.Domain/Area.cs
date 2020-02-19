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

        public IEnumerable<GeoPoint> GeoPoints { get; set; }

        public IEnumerable<File> Files { get; set; }

        public double Progress => Files == null || Files.Count() == 0 ? 0 : Files.Count() / 100;

        public Area(Guid id, Guid mapId)
        {
            Id = id;
            MapId = mapId;
        }
    }
}
