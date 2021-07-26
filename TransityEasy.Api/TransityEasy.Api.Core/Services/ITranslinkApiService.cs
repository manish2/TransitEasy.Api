using System.Collections.Generic;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Services
{
    public interface ITranslinkApiService
    {
        Task<StopsResponseResult> GetNearbyStops(double latitude, double longitude, int radius);
        Task<List<StopEstimatesReponseInfo>> GetNextBusSchedules(int stopNumber, int numNextBuses); 
    }
}
