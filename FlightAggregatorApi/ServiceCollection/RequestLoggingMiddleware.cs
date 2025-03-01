using System.Diagnostics;

namespace FlightAggregatorApi.ServiceCollection
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Фильтруем запросы, начинающиеся с /api
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                var stopwatch = Stopwatch.StartNew();

                var queryParams = context.Request.QueryString.HasValue
                                  ? context.Request.QueryString.Value
                                  : string.Empty;

                _logger.LogInformation("API Request: {Method} {Path}{Query}",
                    context.Request.Method,
                    context.Request.Path,
                    queryParams);

                await _next(context);

                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;
                var statusCode = context.Response.StatusCode;

                _logger.LogInformation("API Response: {Method} {Path}{Query} responded with {StatusCode} in {ElapsedMilliseconds} ms",
                    context.Request.Method,
                    context.Request.Path,
                    queryParams,
                    statusCode,
                    elapsedMs);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
