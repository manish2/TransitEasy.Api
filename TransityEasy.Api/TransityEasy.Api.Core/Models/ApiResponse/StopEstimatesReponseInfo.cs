using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.ApiResponse
{
    public class StopEstimatesReponseInfo
    {
        public string RouteNo { get; set; }
        public string RouteName { get; set; }
        public string Direction { get; set; }
        public StopEstimatesRouteMap RouteMap { get; set; }
        public List<StopEstimatesSchedules> Schedules { get; set; }
    }
}
