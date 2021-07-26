using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.Result
{
    public class NextBusStopInfo
    {
        public string RouteDescription { get; set; }
        public string Direction { get; set; }
        public IEnumerable<NextBusSchedule> Schedules { get; set; }
    }
}
