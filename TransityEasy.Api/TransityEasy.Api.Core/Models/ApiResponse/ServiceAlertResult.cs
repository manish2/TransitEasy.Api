using System;
namespace TransityEasy.Api.Core.Models.ApiResponse
{
    public class ServiceAlertResult
    {
        public Guid Id { get; set; }
        public int Group { get; set; }
        public bool IsClosed { get; set; }
        public bool IsCritical { get; set; }
        public bool IsAdvisory { get; set; }
        public string RouteId { get; set; }
        public string RouteLongName { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }

        public string Certanity { get; set; }
        public string Effect { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string AlertText { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
    }
}
