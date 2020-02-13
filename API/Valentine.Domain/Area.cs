using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class Area
    {
        public Guid Id { get; set; }

        public List<Guid> ImagesIds { get; set; } = new List<Guid>();

        public double Progress => ImagesIds.Count == 0 ? 0 : ImagesIds.Count / 100; 
    }
}
