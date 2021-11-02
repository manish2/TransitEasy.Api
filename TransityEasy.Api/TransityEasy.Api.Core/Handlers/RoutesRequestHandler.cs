using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.CsvRecords;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class RoutesRequestHandler : IRequestHandler<RoutesInfoResult>
    {
        public Task<RoutesInfoResult> HandleRequest()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();
            using var stream = assembly.GetManifestResourceStream("TransityEasy.Api.Core.Resources.routes.txt");
            using var reader = new StreamReader(stream);
            using var csvDecoder = new CSVDecoderService(reader);
            var routeRecords = csvDecoder.GetRecords<Route>();
            var mappedRoutes = routeRecords
                .Select(routeRecord => new RoutesInfo { RouteId = routeRecord.route_id, RouteLongName = routeRecord.route_long_name, RouteShortName = routeRecord.route_short_name })
                .Where(lri => !string.IsNullOrEmpty(lri.RouteShortName))
                .OrderBy(lri => lri.RouteShortName)
                .ToList();

            var result = new RoutesInfoResult { ResponseStatus = StatusCode.Success, RoutesInfo = mappedRoutes };
            return Task.FromResult(result);
        }
    }
}
