using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Models.Result
{
    public class VehiclesLocationResult
    {
        [JsonProperty("vehicleLocations")]
        public IEnumerable<VehicleLocation> VehicleLocations { get; set; } = new List<VehicleLocation>(); 
        [JsonProperty("responseStatus")]
        public StatusCode ResponseStatus { get; set; }
    }
}
