using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Models.Result
{
    public class NextBusStopInfoResult
    {
        public IEnumerable<NextBusStopInfo> NextBusStopInfo { get; set; }
        public StatusCode ResponseStatus { get; set; }
    }
}
