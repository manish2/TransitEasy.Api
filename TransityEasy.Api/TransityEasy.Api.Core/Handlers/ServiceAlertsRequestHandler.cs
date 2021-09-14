using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class ServiceAlertsRequestHandler : IRequestHandler<ServiceAlertsInfo>
    {
        private readonly ITranslinkApiService _translinkApiService;

        public ServiceAlertsRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }

        public async Task<ServiceAlertsInfo> HandleRequest()
        {
            var serviceAlerts = await _translinkApiService.GetServiceAlerts();
            var busAlerts = serviceAlerts
                            .Where(sa => sa.Group == 3)
                            .GroupBy(sa => $"{sa.RouteId} {sa.RouteLongName}")
                            .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            var westCoastExpressAlerts = serviceAlerts.Where(sa => sa.Group == 2).Select(ConvertToServiceAlert);
            var seaBusAlerts = serviceAlerts.Where(sa => sa.Group == 4).Select(ConvertToServiceAlert);
            var stationAccessAlerts = serviceAlerts
                                     .Where(sa => sa.Group == 6)
                                     .GroupBy(sa => sa.StationName)
                                     .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            var skytrainAlerts = serviceAlerts
                                .Where(sa => sa.Group == 1)
                                .GroupBy(sa => $"{sa.RouteId} {sa.RouteLongName}")
                                .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            return new ServiceAlertsInfo
            {
                BusAlerts = busAlerts,
                WestCoastExpressAlerts = westCoastExpressAlerts,
                SeaBusAlerts = seaBusAlerts,
                StationAccessAlerts = stationAccessAlerts,
                SkytrainAlerts = skytrainAlerts
            }; 
        }

        private ServiceAlert ConvertToServiceAlert(IGrouping<string, ServiceAlertResult> serviceAlertGroup)
        {
            return new ServiceAlert
            {
                Count = serviceAlertGroup.Count(),

            }; 
        }

        private ServiceAlert ConvertToServiceAlert(ServiceAlertResult result)
        {
            return new ServiceAlert
            {

            }; 
        }
    }
}
