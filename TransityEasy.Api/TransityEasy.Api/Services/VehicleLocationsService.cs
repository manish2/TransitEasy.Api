using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TransityEasy.Api.Core.Services;
using TransityEasy.Api.Protos;

namespace TransityEasy.Api.Services
{
    public class VehicleLocationsService : VehiclesLocationData.VehiclesLocationDataBase
    {
        private readonly ITranslinkApiService _translinkApiService;
        private readonly ILogger<VehicleLocationsService> _logger; 
        public VehicleLocationsService(ITranslinkApiService translinkApiService, ILogger<VehicleLocationsService> logger)
        {
            _translinkApiService = translinkApiService;
            _logger = logger; 
        }
        public override async Task GetLocations(VehiclesLocationRequest request, IServerStreamWriter<VehiclesLocationResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Handling Request for GetLocations");
            do
            {
                try
                {
                    var isByRoute = request.RouteNo > 1;
                    var apiResponse = isByRoute ? await _translinkApiService.GetVehiclePositionsByRouteNo(request.RouteNo) : await _translinkApiService.GetVehiclePositionsByStopNo(request.StopNumber);
                    if(apiResponse.Code.HasValue)
                    {
                        var errorResponse = new VehiclesLocationResponse();
                        errorResponse.Status = VehiclesLocationResponse.Types.StatusCode.NoneFound;
                        await responseStream.WriteAsync(errorResponse);
                    }
                    else
                    {
                        var response = new VehiclesLocationResponse();
                        response.Status = VehiclesLocationResponse.Types.StatusCode.Success;
                        var locations = apiResponse.LocationsResponseInfo.Select(lri => new VehiclePosition { Latitude = lri.Latitude, Longitude = lri.Longitude, Destination = lri.Destination, Direction = lri.Direction, Pattern = lri.Pattern, TripId = lri.TripId });

                        foreach (var location in locations)
                        {
                            response.Positions.Add(location);
                        }
                        await responseStream.WriteAsync(response);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(request.RefreshTimeInSec));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed GetLocations call with parameters routeNo: {request.RouteNo}, stopNo: {request.StopNumber}, refresh: {request.RefreshTimeInSec}");
                }
            }
            while (!context.CancellationToken.IsCancellationRequested); 
        }
    }
}
