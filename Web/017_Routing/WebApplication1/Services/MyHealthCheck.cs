using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.Services
{
    public class MyHealthCheck : IHealthCheck
    {
        private readonly Random _random = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return this._random.Next(2) < 1 ? Task.FromResult(HealthCheckResult.Healthy()) : Task.FromResult(HealthCheckResult.Degraded());
        }
    }
}