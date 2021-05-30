using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TransityEasy.Api.Core.Options;
using TransityEasy.Api.Core.Models.ApiResponse;
using System.Net.Http.Json;
using System.Collections.Generic;

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

        public async Task<List<StopsResponse>> GetNearbyStops(double latitude, double longitude, int radius)
        {
            var url = $"/rttiapi/v1/stops?apiKey={_translinkOptions.Value.ApiKey}&lat={latitude}&long={longitude}&radius={radius}";
            return await _httpClient.GetFromJsonAsync<List<StopsResponse>>(url);
        }
    }
}
