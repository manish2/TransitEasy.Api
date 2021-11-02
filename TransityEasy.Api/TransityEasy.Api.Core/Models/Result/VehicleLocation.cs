using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class VehicleLocation
    {
		[JsonProperty("latitude")]
		public double Latitude { get; set; }
		[JsonProperty("longitude")]
		public double Longitude { get; set; }
		[JsonProperty("vehicleNo")]
		public string VehicleNo { get; set; }
		[JsonProperty("tripId")]
		public int TripId { get; set; }
		[JsonProperty("routeNo")]
		public int RouteNo { get; set; }
		[JsonProperty("direction")]
		public string Direction { get; set; }
		[JsonProperty("destination")]
		public string Destination { get; set; }
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
	}
}
