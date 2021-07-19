using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Enums;

namespace Valentine.Application.Models
{
    public class Geometry : Feature
    { 
        public IEnumerable<IEnumerable<double[]>> Coordinates { get; set; }

        public Geometry(FeatureType type, IEnumerable<double[]> coordinates)
        {
            Type = type;
            Coordinates = Enumerable.Repeat(coordinates, 1);
        }
    }
}
