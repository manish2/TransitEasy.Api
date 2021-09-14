using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Result;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransityEasy.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ServiceAlertsController : ControllerBase
    {
        private readonly IRequestHandler<ServiceAlertsInfo> _serviceAlertsRequestHandler; 

        public ServiceAlertsController(IRequestHandler<ServiceAlertsInfo> serviceAlertsRequestHandler)
        {
            _serviceAlertsRequestHandler = serviceAlertsRequestHandler; 
        }

        [HttpGet("servicealerts")]
        public async Task<ServiceAlertsInfo> GetServiceAlerts()
        {
            return await _serviceAlertsRequestHandler.HandleRequest(); 
        }
    }
}
