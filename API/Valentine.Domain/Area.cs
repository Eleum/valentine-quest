using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class Area
    {
        public Guid Id { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public double Progress => Images.Count == 0 ? 0 : Images.Count / 100; 
    }
}
