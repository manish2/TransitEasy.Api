using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransityEasy.Api.Middleware
{
    public class HttpRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public HttpRequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<HttpRequestLoggingMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {protocol} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Protocol,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
