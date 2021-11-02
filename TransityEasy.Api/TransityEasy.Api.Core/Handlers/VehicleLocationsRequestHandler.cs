using System;
using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class VehicleLocationsRequestHandler : IRequestHandler<VehiclesLocationRequest, VehiclesLocationResult>
    {
        private readonly ITranslinkApiService _translinkApiService; 
        public VehicleLocationsRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }
        public async Task<VehiclesLocationResult> HandleRequest(VehiclesLocationRequest request)
        {
            var isByRouteNo = request.RouteNo.HasValue;
            var result =  await (isByRouteNo ? _translinkApiService.GetVehiclePositionsByRouteNo(request.RouteNo.Value) : _translinkApiService.GetVehiclePositionsByRouteNo(request.StopNo.Value));
            if(result.Code.HasValue)
            {
                return new VehiclesLocationResult {
                    ResponseStatus = SetStatus(result.Code.Value)
                };
            }
            var mappedLocations = result.LocationsResponseInfo.Select(MapToVehicleLocation);

            return new VehiclesLocationResult {
                VehicleLocations = mappedLocations
            }; 
        }
        private StatusCode SetStatus(TranslinkApiErrorCodes code)
        {
            return code switch
            {
                TranslinkApiErrorCodes.NoBusesFound => StatusCode.NoVehiclesAvailable,
                _ => StatusCode.NoVehiclesAvailable,
            };
        }
        private VehicleLocation MapToVehicleLocation(BusLocationResponseInfo info)
        {
            return new VehicleLocation { 
                Latitude = info.Latitude,
                Longitude = info.Longitude,
                Pattern = info.Pattern,
                TripId = info.TripId,
                VehicleNo = info.VehicleNo,
                Destination = info.Destination,
                Direction = info.Direction
            }; 
        }
    }
}
