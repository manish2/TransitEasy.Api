
using System.Collections.Generic;

namespace TransityEasy.Api.Core.Models.Result
{
    public class ServiceAlertsInfo
    {
        public Dictionary<string, ServiceAlert> BusAlerts { get; set; }
        public Dictionary<string, ServiceAlert> SkytrainAlerts { get; set; }
        public ServiceAlert WestCoastExpressAlerts { get; set; }
        public ServiceAlert SeaBusAlerts { get; set; }
        public ServiceAlert HandyDARTAlerts { get; set; }
        public Dictionary<string, ServiceAlert> StationAccessAlerts { get; set; }
        public ServiceAlert InformationServicesAlerts { get; set; }
    }
}
