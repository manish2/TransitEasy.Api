using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.Result
{
    public class ServiceAlert
    {
        public int Count { get; set; }
        public IEnumerable<Alert> Alerts { get; set; }
    }
}
