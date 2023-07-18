using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using System.Text.Json;
using NLog;
using System.Net;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace WeatherMap_api.Middlewares
{
    public class RateLimitMiddleware : ClientRateLimitMiddleware
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;
        private readonly IConfiguration _appconfig;

        public RateLimitMiddleware(RequestDelegate next,
            IProcessingStrategy processingStrategy,
            IOptions<ClientRateLimitOptions> options,
            IClientPolicyStore policyStore,
            IRateLimitConfiguration config,
            ILogger<ClientRateLimitMiddleware> logger, IConfiguration appconfig) :
            base(next, processingStrategy, options, policyStore, config, logger)
        { _next = next; _appconfig = appconfig; }

        public override async Task<Task> ReturnQuotaExceededResponse
       (HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            string attemptsFile = @"helper/attempts.txt";

            var count = File.ReadLines(attemptsFile).Count();

            int rateCount = short.Parse(_appconfig["RateLimitCount"]) + 1;

            if (httpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                //The RateLimitCount is set to 5, the 6th attempt should fail, StatusCode 429 will be sent back to ui
                //UI will use the next rotate key for 6 more attempts
                if (count % rateCount != 0) 
                {
                    await _next(httpContext);
                    return Task.CompletedTask;
                }
            }

            File.WriteAllText(attemptsFile, string.Empty);
            string? path = httpContext?.Request?.Path.Value;
            httpContext.Response.StatusCode = 429;
            httpContext.Response.ContentType = "application/json";

            WriteQuotaExceededResponseMetadata(path, retryAfter);

            _logger.Warn(string.Format("{0} API rate limit quota exceeded {1}", 429, httpContext.Request.Path));

            return httpContext.Response.WriteAsync("API hourly rate limit quota exceeded");

        }
        private void WriteQuotaExceededResponseMetadata
        (string requestPath, string retryAfter, int statusCode = 429)
        {
            //Code to write data to the database
        }
    }
}
