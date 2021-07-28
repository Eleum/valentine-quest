using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.Shared.Contracts.Models
{
    public class AreaModel
    {
        public Guid Id { get; set; }

        public double Progress { get; set; }

        public IEnumerable<GeoPointModel> GeoPoints { get; set; }
    }
}
