using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;

namespace TransityEasy.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class StopsController : ControllerBase
    {
        private readonly IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult> _nearbyStopsInfoRequestHandler;
        private readonly IRequestHandler<NextBusesScheduleRequest, NextBusStopInfoResult> _nextBusScheduleRequestHandler; 

        public StopsController(IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult> nearbyStopsInfoRequestHandler)
        {
            _nearbyStopsInfoRequestHandler = nearbyStopsInfoRequestHandler;
        }

        [HttpGet("getnearbystops")]
        public async Task<ActionResult<NearbyStopsInfoResult>> GetNearbyStops(double currentLat, double currentLong, int radius)
        {
            try
            {
                var request = new NearbyStopsInfoRequest
                {
                    Latitude = currentLat,
                    Longitude = currentLong,
                    Radius = radius
                };
                var result = await _nearbyStopsInfoRequestHandler.HandleRequest(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getnextbusschedules")]
        public async Task<ActionResult<NextBusStopInfoResult>> GetNextBusSchedules(int stopNumber, int numNextBuses)
        {
            try
            {
                var request = new NextBusesScheduleRequest
                {
                    StopNumber = stopNumber,
                    NumNextBuses = numNextBuses
                }; 
                var result = await _nextBusScheduleRequestHandler.HandleRequest(request);
                return Ok(result); 
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
