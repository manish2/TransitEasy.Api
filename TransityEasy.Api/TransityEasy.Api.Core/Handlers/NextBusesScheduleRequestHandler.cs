using System.Linq;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Models.ApiResponse;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api.Core.Handlers
{
    public class NextBusesScheduleRequestHandler : IRequestHandler<NextBusesScheduleRequest, NextBusStopInfoResult>
    {
        private readonly ITranslinkApiService _translinkApiService;

        public NextBusesScheduleRequestHandler(ITranslinkApiService translinkApiService)
        {
            _translinkApiService = translinkApiService; 
        }

        public async Task<NextBusStopInfoResult> HandleRequest(NextBusesScheduleRequest request)
        {
            var translinkData = await _translinkApiService.GetNextBusSchedules(request.StopNumber, request.NumNextBuses);
            var nextBusInfo = translinkData.Select(td => ConvertStopEstimateFromApiResult(td));

            return new NextBusStopInfoResult
            {
                NextBusStopInfo = nextBusInfo,
                ResponseStatus = StatusCode.Success
            }; 
        }

        private NextBusStopInfo ConvertStopEstimateFromApiResult(StopEstimatesReponseInfo stopEstimatesResponseInfo)
        {
            var convertedSchedule = stopEstimatesResponseInfo.Schedules.Select(schedule => ConvertScheduleFromApiResult(schedule)); 
            return new NextBusStopInfo
            {
                RouteDescription = $"{stopEstimatesResponseInfo.RouteNo} - {stopEstimatesResponseInfo.RouteName}",
                Direction = stopEstimatesResponseInfo.Direction,
                Schedules = convertedSchedule
            };
        }

        private NextBusSchedule ConvertScheduleFromApiResult(StopEstimatesSchedules schedule)
        {
            return new NextBusSchedule
            {
                IsStopCancelled = schedule.CancelledStop,
                IsTripCancelled = schedule.CancelledTrip,
                CountdownInMin = schedule.ExpectedCountdown,
                ScheduleStatus = MapToStatus(schedule.ScheduleStatus),
                Destination = schedule.Destination
            }; 
        }
        private NextBusScheduleStatus MapToStatus(string statusInResponse)
        {
            return statusInResponse switch
            {
                "-" => NextBusScheduleStatus.DELAYED,
                "+" => NextBusScheduleStatus.AHEAD,
                _ => NextBusScheduleStatus.ONTIME,
            };
        }
    }
}
