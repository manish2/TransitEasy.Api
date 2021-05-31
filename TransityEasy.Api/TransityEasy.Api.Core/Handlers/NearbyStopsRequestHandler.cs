using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class NearbyStopsRequestHandler : IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult>
    {
        private readonly ITranslinkApiService _translinkApiService; 
        public NearbyStopsRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }
        public async Task<NearbyStopsInfoResult> HandleRequest(NearbyStopsInfoRequest request)
        {
            var apiResult = await _translinkApiService.GetNearbyStops(request.Latitude, request.Longitude, request.Radius);

            if(apiResult.Code.HasValue)
                return new NearbyStopsInfoResult {
                    NearbyStopsInfo = Enumerable.Empty<NearbyStopsInfo>(),
                    ResponseStatus = MapResponseCodes(apiResult.Code.Value)
                }; 

            var stopsInfo = apiResult.StopsResponseInfo.Select(ConvertFromApiResult);
            return new NearbyStopsInfoResult
            {
                NearbyStopsInfo = stopsInfo,
                ResponseStatus = StatusCode.Success
            }; 
        }

        private NearbyStopsInfo ConvertFromApiResult(StopsResponseInfo response)
        {
            return new NearbyStopsInfo
            {
                StopNo = response.StopNo,
                Latitude = response.Latitude,
                Longitude = response.Longitude,
                StopName = response.Name,
                BayNo = response.BayNo,
                IsWheelchairAccessible = response.WheelchairAccess == 1,
                Distance = response.Distance,
                Routes = ConvertCsvStringToList(response.Routes)
            }; 
        }
        private StatusCode MapResponseCodes(TranslinkApiErrorCodes errorCode)
        {
            switch(errorCode)
            {
                case TranslinkApiErrorCodes.NoStopsFound:
                    return StatusCode.NoStopsNearLocation;
                default:
                    return StatusCode.Success; 
            }
        }
        private IEnumerable<string> ConvertCsvStringToList(string csv) => csv.Split(',').Select(s => s.Trim()); 
    }
}
