using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private IDisposable? _scheduler;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;

            static void Callback(object? state) => Console.WriteLine($"[{DateTimeOffset.Now}]{PerformanceMetrics.Create()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}