using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class AlertsRequestHandler : IRequestHander<ServiceAlertsInfo>
    {
        private readonly ITranslinkApiService _translinkApiService;

        public AlertsRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }

        public async Task<ServiceAlertsInfo> HandleRequest()
        {
            var serviceAlerts = await _translinkApiService.GetServiceAlerts();
            var busAlerts = serviceAlerts.Where(sa => sa.Group == 3).Select(ConvertToServiceAlert);
            var westCoastExpressAlerts = serviceAlerts.Where(sa => sa.Group == 2).Select(ConvertToServiceAlert);
            var seaBusAlerts = serviceAlerts.Where(sa => sa.Group == 4).Select(ConvertToServiceAlert);
            var stationAccessAlerts = serviceAlerts.Where(sa => sa.Group == 6).Select(ConvertToServiceAlert);
            var skytrainAlerts = serviceAlerts.Where(sa => sa.Group == 1).Select(ConvertToServiceAlert);

            return new ServiceAlertsInfo
            {
                BusAlerts = busAlerts,
                WestCoastExpressAlerts = westCoastExpressAlerts,
                SeaBusAlerts = seaBusAlerts,
                StationAccessAlerts = stationAccessAlerts,
                SkytrainAlerts = skytrainAlerts
            }; 
        }

        private ServiceAlert ConvertToServiceAlert(ServiceAlertResult serviceAlertResult)
        {
            return new ServiceAlert
            {

            }; 
        }
    }
}
