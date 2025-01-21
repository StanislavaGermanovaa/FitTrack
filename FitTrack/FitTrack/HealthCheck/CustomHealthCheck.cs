using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FitTrack.HealthCheck
{
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The system is healthy."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("The system is unhealthy."));
            }
        }
    }
}
