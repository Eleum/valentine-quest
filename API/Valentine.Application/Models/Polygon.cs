using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Enums;
using Valentine.Application.Interfaces;

namespace Valentine.Application.Models
{
    public class Polygon : Feature
    {
        public Dictionary<string, object> Properties { get; set; }

        public Geometry Geometry { get; set; }
    }
}
