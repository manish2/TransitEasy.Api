using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class NearbyStopsRequestHandler : IRequestHandler<NearbyStopsInfoRequest, IEnumerable<NearbyStopsInfoResult>>
    {
        private readonly ITranslinkApiService _translinkApiService; 
        public NearbyStopsRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }
        public async Task<IEnumerable<NearbyStopsInfoResult>> HandleRequest(NearbyStopsInfoRequest request)
        {
            var apiResult = await _translinkApiService.GetNearbyStops(request.Latitude, request.Longitude, request.Radius);
            return apiResult.Select(ConvertFromApiResult); 
        }

        private NearbyStopsInfoResult ConvertFromApiResult(StopsResponse response)
        {
            return new NearbyStopsInfoResult
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

        private IEnumerable<string> ConvertCsvStringToList(string csv) => csv.Split(',').Select(s => s.Trim()); 
    }
}
