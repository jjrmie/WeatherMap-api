using Microsoft.AspNetCore.DataProtection.KeyManagement;
using NLog;
using System.Net;

namespace API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _apiKey = "ApiKey"; //Key in appSettings
        private readonly IConfiguration _config;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_apiKey, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _logger.Warn(string.Format("{0} Unauthorized - Api Key was not provided {1}", (int)HttpStatusCode.Unauthorized, context.Request.Path));
                await context.Response.WriteAsync("Unauthorized - Api Key was not provided ");
                return;
            }

            var apiKey = _config[_apiKey];
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _logger.Warn(string.Format("{0} Unauthorized - Incorrect Api Key was provided {1}", (int)HttpStatusCode.Unauthorized, context.Request.Path));
                await context.Response.WriteAsync("Unauthorized - Incorrect Api Key was provided ");
                return;
            }
            await _next(context);
        }
    }
}
