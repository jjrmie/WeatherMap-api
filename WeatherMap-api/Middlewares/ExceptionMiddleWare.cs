using Microsoft.AspNetCore.Http;
using NLog;
using System.Net;
using System.Text.Json;

namespace WeatherMap_api.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ExceptionMiddleWare(RequestDelegate next, IHostEnvironment env)
        {
            _env = env;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, string.Format("{0} {1} {2} {3}", (int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString(), context.Request.Path));

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(context.Response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
