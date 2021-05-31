using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Models.Result
{
    public class NearbyStopsInfoResult
    {
        public IEnumerable<NearbyStopsInfo> NearbyStopsInfo { get; set; }
        public StatusCode ResponseStatus { get; set; }
    }
}
