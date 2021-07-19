using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Enums;
using Valentine.Application.Interfaces;

namespace Valentine.Application.Models
{
    public class FeatureCollection<T> : Feature where T: Feature
    {
        public IEnumerable<T> Features { get; set; }
    }
}
