using System.Collections.Generic;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.Result;

namespace TransityEasy.Api.Core.Services
{
    public interface IKmlDecoderService
    {
        Task<IEnumerable<IEnumerable<RouteMapLatLng>>> DecodeKMZFromURL(string url); 
    }
}
