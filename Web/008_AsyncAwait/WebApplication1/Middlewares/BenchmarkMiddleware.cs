using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BenchmarkMiddleware
    {
        private static List<long> timeList = new List<long>();
        private readonly RequestDelegate _next;
        private readonly ILogger<BenchmarkMiddleware> _logger;

        public BenchmarkMiddleware(RequestDelegate next, ILogger<BenchmarkMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(httpContext);

            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds;
            timeList.Add(time);
            var count = timeList.Count;
            var avg = timeList.Sum() / count;
            _logger.LogWarning($"time:{time}, times: {count}, avg:{avg}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BenchmarkMiddlewareExtensions
    {
        public static IApplicationBuilder UseBenchmarkMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BenchmarkMiddleware>();
        }
    }
}