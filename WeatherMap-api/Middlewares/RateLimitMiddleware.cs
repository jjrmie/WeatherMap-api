using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using System.Text.Json;
using NLog;
using System.Net;

namespace WeatherMap_api.Middlewares
{
    public class RateLimitMiddleware : ClientRateLimitMiddleware
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public RateLimitMiddleware(RequestDelegate next,
            IProcessingStrategy processingStrategy,
            IOptions<ClientRateLimitOptions> options,
            IClientPolicyStore policyStore,
            IRateLimitConfiguration config,
            ILogger<ClientRateLimitMiddleware> logger) :
            base(next, processingStrategy, options, policyStore, config, logger)
        {}

        public override Task ReturnQuotaExceededResponse
       (HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            string? path = httpContext?.Request?.Path.Value;
            var result = JsonSerializer.Serialize("API rate limit quota exceeded");
            httpContext.Response.Headers["Retry-After"] = retryAfter;
            httpContext.Response.StatusCode = 429;
            httpContext.Response.ContentType = "application/json";

            WriteQuotaExceededResponseMetadata(path, retryAfter);

            _logger.Warn(string.Format("{0} API rate limit quota exceeded {1}", 429, httpContext.Request.Path));

            return httpContext.Response.WriteAsync(result);

        }
        private void WriteQuotaExceededResponseMetadata
        (string requestPath, string retryAfter, int statusCode = 429)
        {
            //Code to write data to the database
        }
    }
}
