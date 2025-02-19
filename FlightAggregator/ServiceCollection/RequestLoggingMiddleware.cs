namespace FlightAggregator.ServiceCollection;

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
        // Log request details
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            _logger.LogInformation("API Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        }

        await _next(context);
    }
}



