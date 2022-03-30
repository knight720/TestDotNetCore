using System.Diagnostics;

namespace WebAPI.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            this._logger.LogInformation($"{context.Request.Path} - Start");

            // Call the next delegate/middleware in the pipeline.
            await _next(context);

            sw.Stop();
            this._logger.LogInformation($"{context.Request.Path} - End - {sw.ElapsedMilliseconds}ms");
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddlware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}