using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private readonly IKmlDecoderService _kmlDecoderService;
        private readonly ConcurrentDictionary<string, IEnumerable<IEnumerable<RouteMapLatLng>>> _routeMapCache; 
        public VehicleLocationsRequestHandler(ITranslinkApiService translinkApiService, IKmlDecoderService kmlDecoderService)
        {
            _translinkApiService = translinkApiService;
            _kmlDecoderService = kmlDecoderService;
            _routeMapCache = new ConcurrentDictionary<string, IEnumerable<IEnumerable<RouteMapLatLng>>>(); 
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
            Task.WaitAll(mappedLocations.ToArray());

            return new VehiclesLocationResult {
                VehicleLocations = mappedLocations.Select(t => t.Result)
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
        private async Task<VehicleLocation> MapToVehicleLocation(BusLocationResponseInfo info)
        {
            IEnumerable<IEnumerable<RouteMapLatLng>> latLongData;
            if (_routeMapCache.ContainsKey(info.RouteMap.Href))
            {
                latLongData = _routeMapCache[info.RouteMap.Href];
            }
            else
            {
                latLongData = await _kmlDecoderService.DecodeKMZFromURL(info.RouteMap.Href);
                _routeMapCache.TryAdd(info.RouteMap.Href, latLongData);
            }

            return new VehicleLocation { 
                Latitude = info.Latitude,
                Longitude = info.Longitude,
                Pattern = info.Pattern,
                TripId = info.TripId,
                VehicleNo = info.VehicleNo,
                Destination = info.Destination,
                Direction = info.Direction,
                RouteMapData = new RouteMapData
                {
                    CoordinateData = latLongData
                }
            }; 
        }
    }
}
