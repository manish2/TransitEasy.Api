using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class Alert
    {
        public Guid AlertId { get; set; }
        public string RouteId { get; set; }
        public string RouteLongName { get; set; }
        public string Effect { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string AlertText { get; set; }
        public string AlertHeader { get; set; }
        public string AlertDescription { get; set; }
    }
}
