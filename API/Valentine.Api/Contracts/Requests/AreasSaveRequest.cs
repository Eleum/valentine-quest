using System.Collections.Generic;

namespace Valentine.Api.Contracts.Requests
{
    public class AreasSaveRequest
    {
        public IEnumerable<AreasSaveRequestModel> Data { get; set; }
    }

    public class AreasSaveRequestModel
    {
        public string AreaId { get; set; }

        public int Position { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
