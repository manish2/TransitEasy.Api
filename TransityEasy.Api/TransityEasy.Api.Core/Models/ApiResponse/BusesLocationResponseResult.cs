using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.ApiResponse
{
    public class BusesLocationResponseResult
    {
        public List<BusLocationResponseInfo> LocationsResponseInfo { get; set; }
        public TranslinkApiErrorCodes? Code { get; set; }
        public string Message { get; set; }
    }
}
