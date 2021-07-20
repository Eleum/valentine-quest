using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Client.Helpers
{
    public class Constants
    {
        public static class Api
        {
            public const string INTERNAL_API = "InternalApi";
            public const string WEB_API = "WebApi";
        }

        public static class Coordinates
        {
            public static readonly double[] NE = new[] { 26.955186661198155, 53.76002532703791 }; // Southwest coordinates
            public static readonly double[] SW = new[] { 28.067600169786317, 54.08218624607514 }; // Northeast coordinates
        }
    }
}
