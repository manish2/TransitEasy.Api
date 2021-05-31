using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Options;

namespace TransityEasy.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class StopsController : ControllerBase
    {
        private readonly IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult> _nearbyStopsInfoRequestHandler;

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
    }
}
