using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class RouteMapLatLng
    {
        [JsonProperty("routeMapData")]
        public double Latitude { get; set; }
        [JsonProperty("routeMapData")]
        public double Longitude { get; set; }
    }
}
