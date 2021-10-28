using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Result;

namespace TransityEasy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRequestHandler<RoutesInfoResult> _requestHandler;
        private readonly IHostApplicationLifetime _applicationLifeTime;
        public RoutesController(IRequestHandler<RoutesInfoResult> requestHandler, IHostApplicationLifetime applicationLifetime)
        {
            _requestHandler = requestHandler;
            _applicationLifeTime = applicationLifetime; 
        }

        [HttpGet]
        public async Task<RoutesInfoResult> GetAllRoutes()
        {
            return await _requestHandler.HandleRequest();
        }
    }
}
