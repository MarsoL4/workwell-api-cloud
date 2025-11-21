using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.API.HealthChecks
{
    public static class HealthChecksExtension
    {
        public static IServiceCollection AddWorkWellHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<WorkWellDbContext>(
                    name: "Database",
                    failureStatus: HealthStatus.Unhealthy);

            return services;
        }
    }
}