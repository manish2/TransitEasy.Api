using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.ApiResponse
{
    public class StopsResponseResult
    {
        public List<StopsResponseInfo> StopsResponseInfo { get; set; }
        public TranslinkApiErrorCodes? Code { get; set; }
        public string Message { get; set; }
    }
}
