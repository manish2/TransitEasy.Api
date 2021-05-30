using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;

namespace TransityEasy.Api.Core.Services
{
    public interface ITranslinkApiService
    {
        Task<List<StopsResponse>> GetNearbyStops(double latitude, double longitude, int radius); 
    }
}
