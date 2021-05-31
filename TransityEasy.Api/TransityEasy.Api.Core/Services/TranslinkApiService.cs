using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TransityEasy.Api.Core.Options;
using TransityEasy.Api.Core.Models.ApiResponse;
using System.Collections.Generic;
using TransityEasy.Api.Core.Extensions;
using System.Net;
using Newtonsoft.Json;

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
            var url = $"/rttiapi/v1/stops?apiKey={_translinkOptions.Value.ApiKey}&lat={latitude}&long={longitude}&radius={radius}";
            (var payload, var status) = await _httpClient.GetPayloadWithHttpCodeAsync(url, new HashSet<HttpStatusCode> { HttpStatusCode.NotFound });

            if (status == HttpStatusCode.NotFound)
                return JsonConvert.DeserializeObject<StopsResponseResult>(payload);

            var data = JsonConvert.DeserializeObject<List<StopsResponseInfo>>(payload);

            return new StopsResponseResult
            {
                StopsResponseInfo = data
            }; 
        }
    }
}
