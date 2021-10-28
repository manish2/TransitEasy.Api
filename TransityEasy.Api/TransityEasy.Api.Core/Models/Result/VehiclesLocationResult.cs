using System;
using System.Collections.Generic;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Models.Result
{
    public class VehiclesLocationResult
    {
        public IEnumerable<VehicleLocation> VehicleLocations { get; set; }
        public StatusCode ResponseStatus { get; set; }
    }
}
