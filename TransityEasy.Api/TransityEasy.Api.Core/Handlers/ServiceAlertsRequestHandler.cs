using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        private readonly ILogger<ServiceAlertsRequestHandler> _logger;

        public ServiceAlertsRequestHandler(ITranslinkApiService translinkApiService, ILogger<ServiceAlertsRequestHandler> logger)
        {
            _translinkApiService = translinkApiService;
            _logger = logger;
        }

        public async Task<ServiceAlertsInfo> HandleRequest()
        {
            _logger.LogInformation("Handling ServiceAlertsInfo Request"); 
            var serviceAlerts = await _translinkApiService.GetServiceAlerts();
            var busAlerts = serviceAlerts
                            .Where(sa => sa.Group == 3)
                            .GroupBy(sa => $"{sa.RouteId} {sa.RouteLongName}")
                            .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            var stationAccessAlerts = serviceAlerts
                         .Where(sa => sa.Group == 6)
                         .GroupBy(sa => sa.StationName)
                         .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            var skytrainAlerts = serviceAlerts
                                .Where(sa => sa.Group == 1)
                                .GroupBy(sa => $"{sa.RouteId} {sa.RouteLongName}")
                                .ToDictionary(group => group.Key, group => ConvertToServiceAlert(group));

            var westCoastExpressAlerts = ConvertToServiceAlert(serviceAlerts.Where(sa => sa.Group == 2));
            var seaBusAlerts = ConvertToServiceAlert(serviceAlerts.Where(sa => sa.Group == 4));

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
                Alerts = serviceAlertGroup.Select(ConvertToAlert)
            }; 
        }
        private Alert ConvertToAlert(ServiceAlertResult result)
        {
            return new Alert
            {
                AlertId = result.Id,
                RouteId = result.RouteId,
                RouteLongName = result.RouteLongName,
                AlertText = result.AlertText,
                AlertHeader = result.Header,
                AlertDescription = result.Description,
                Effect = result.Effect,
                StartTime = result.StartTime,
                EndTime = result.EndTime
            }; 
        }
        private ServiceAlert ConvertToServiceAlert(IEnumerable<ServiceAlertResult> result)
        {
            return new ServiceAlert
            {
                Count = result.Count(),
                Alerts = result.Select(ConvertToAlert)
            }; 
        }
    }
}
