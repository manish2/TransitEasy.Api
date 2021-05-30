using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T> GetFromJsonAsync<T>(this HttpClient client, string requestUri)
        {
            var data = await client.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(data); 
        } 
    }
}
