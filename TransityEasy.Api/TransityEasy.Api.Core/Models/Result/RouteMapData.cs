using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class RouteMapData
    {
        [JsonProperty("coordinateData")]
        public IEnumerable<IEnumerable<RouteMapLatLng>> CoordinateData { get; set; }
    }
}
