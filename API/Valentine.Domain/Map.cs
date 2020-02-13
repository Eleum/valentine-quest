using System;
using System.Collections.Generic;
using System.Text;

namespace Valentine.Domain
{
    public class Map
    {
        public Guid Id { get; set; }

        public List<Area> Areas { get; set; }
    }
}
