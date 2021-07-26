using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TransityEasy.Api.Core.Options;
using TransityEasy.Api.Core.Models.ApiResponse;
using System.Collections.Generic;
using TransityEasy.Api.Core.Extensions;
using System.Net;
using Newtonsoft.Json;
using System;

namespace TransityEasy.Api.Core.Services
{
    public class TranslinkApiService : ITranslinkApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsSnapshot<TranslinkOptions> _translinkOptions; 
        public TranslinkApiService(HttpClient client, IOptionsSnapshot<TranslinkOptions> translinkOptions)
        {
            _httpClient = client;
            _translinkOptions = translinkOptions; 
        }

        public async Task<StopsResponseResult> GetNearbyStops(double latitude, double longitude, int radius)
        {
            var tranlinkLat = ToTranslinkLatLongFormat(latitude); 
            var translinkLong = ToTranslinkLatLongFormat(longitude);
            var url = $"/rttiapi/v1/stops?apiKey={_translinkOptions.Value.ApiKey}&lat={tranlinkLat}&long={translinkLong}&radius={radius}";
            (var payload, var status) = await _httpClient.GetPayloadWithHttpCodeAsync(url, new HashSet<HttpStatusCode> { HttpStatusCode.NotFound });

            if (status == HttpStatusCode.NotFound)
                return JsonConvert.DeserializeObject<StopsResponseResult>(payload);

            var data = JsonConvert.DeserializeObject<List<StopsResponseInfo>>(payload);

            return new StopsResponseResult
            {
                StopsResponseInfo = data
            }; 
        }

        public async Task<List<StopEstimatesReponseInfo>> GetNextBusSchedules(int stopNumber, int numNextBuses)
        {
            var url = $"/rttiapi/v1/stops/{stopNumber}/estimates?apiKey={_translinkOptions.Value.ApiKey}&count={numNextBuses}";
            (var payload, var status) = await _httpClient.GetPayloadWithHttpCodeAsync(url, null);

            var data = JsonConvert.DeserializeObject<List<StopEstimatesReponseInfo>>(payload);
            return data; 
        }

        //Translink is at max 6 decimal places
        private double ToTranslinkLatLongFormat(double value) => Math.Truncate(1000000 * value) / 1000000;
    }
}
