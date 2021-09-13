
using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.Result
{
    public class ServiceAlertsInfo
    {
        public IEnumerable<ServiceAlert> BusAlerts { get; set; }
        public IEnumerable<ServiceAlert> SkytrainAlerts { get; set; }
        public IEnumerable<ServiceAlert> WestCoastExpressAlerts { get; set; }
        public IEnumerable<ServiceAlert> SeaBusAlerts { get; set; }
        public IEnumerable<ServiceAlert> HandyDARTAlerts { get; set; }
        public IEnumerable<ServiceAlert> StationAccessAlerts { get; set; }
        public IEnumerable<ServiceAlert> InformationServicesAlerts { get; set; }
    }
}
