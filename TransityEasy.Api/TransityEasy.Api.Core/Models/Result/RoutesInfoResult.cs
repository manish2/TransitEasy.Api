using System.Collections.Generic;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Models.Result
{
    public class RoutesInfoResult
    {
        public IEnumerable<RoutesInfo> RoutesInfo { get; set; }
        public StatusCode ResponseStatus { get; set; }
    }
}
