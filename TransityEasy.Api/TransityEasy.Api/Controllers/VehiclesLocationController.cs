using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;

namespace TransityEasy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesLocationController : ControllerBase
    {
        private readonly IRequestHandler<VehiclesLocationRequest, VehiclesLocationResult> _requestHandler;
        public VehiclesLocationController(IRequestHandler<VehiclesLocationRequest, VehiclesLocationResult> requestHandler)
        {
            _requestHandler = requestHandler; 
        }

        [HttpGet("getvehicleslocation")]
        public async Task GetVehiclesLocation(int? stopNo, int? routeNo, int refreshIntervalInSeconds)
        {
            //Reject connection if it is not a websocket request
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            while (!webSocket.CloseStatus.HasValue)
            {
                var result = await _requestHandler.HandleRequest(new VehiclesLocationRequest { StopNo = stopNo, RouteNo = routeNo}).ConfigureAwait(false);
                var dataBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                await webSocket.SendAsync(new ArraySegment<byte>(dataBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(TimeSpan.FromSeconds(refreshIntervalInSeconds));
            }
        }
    }
}
