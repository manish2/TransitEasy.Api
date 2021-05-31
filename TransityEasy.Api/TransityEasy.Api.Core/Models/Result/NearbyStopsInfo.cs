using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class NearbyStopsInfo
    {
        public int StopNo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StopName { get; set; }
        public string BayNo { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public int Distance { get; set; }
        public IEnumerable<string> Routes { get; set; }
    }
}
