using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Enums;

namespace Valentine.Application.Models
{
    public abstract class Feature
    {
        public FeatureType Type { get; set; }
    }
}
