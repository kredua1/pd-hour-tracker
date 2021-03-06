using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Health
{
    internal class ReadinessHealthCheck : IHealthCheck
    {
        private HealthStatusData _healthStatusData;

        public ReadinessHealthCheck(HealthStatusData healthStatusData)
        {
            _healthStatusData = healthStatusData;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_healthStatusData.IsLiveness)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Error"));
            }
        }
    }
}
