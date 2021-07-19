using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentine.Application.Models
{
    public class Area
    {
        public string Id { get; set; }

        public IEnumerable<double[]> Points { get; set; }
    }
}
