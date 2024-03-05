using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

new HostBuilder()
    .ConfigureServices(svcs => svcs
    .AddSingleton<IHostedService, PerformanceMetricsCollector>())
    .Build()
    .Run();