#region Usings
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
#endregion

namespace Vantage.ServiceDefaults
{
    /// <summary>
    /// Centralized extension methods to guarantee identical OpenTelemetry, health check, 
    /// and resilience configurations across all Project Vantage microservices.
    /// </summary>
    public static class ServiceDefaultsExtensions
    {
        #region Public Extensions
        /// <summary>
        /// Registers the standard baseline for telemetry, health checks, and service discovery.
        /// </summary>
        public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ConfigureOpenTelemetry();
            builder.AddDefaultHealthChecks();
            builder.Services.AddServiceDiscovery();
            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                http.AddStandardResilienceHandler();
                http.AddServiceDiscovery();
            });
            return builder;
        }

        /// <summary>
        /// Configures OpenTelemetry logging, metrics, and distributed tracing.
        /// </summary>
        public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Logging.AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.IncludeScopes = true;
            });

            builder.Services.AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics.AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation()
                           .AddRuntimeInstrumentation();
                })
                .WithTracing(tracing =>
                {
                    tracing.AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation();
                });

            builder.AddOpenTelemetryExporters();
            return builder;
        }

        /// <summary>
        /// Registers standard liveness and readiness health checks.
        /// </summary>
        public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Services.AddHealthChecks()
                .AddCheck(HealthDiagnostics.SelfCheckName, () => HealthCheckResult.Healthy(), [HealthDiagnostics.LivenessTag]);
            return builder;
        }

        /// <summary>
        /// Maps the standardized health check endpoints to the application pipeline.
        /// </summary>
        public static WebApplication MapDefaultEndpoints(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.MapHealthChecks(HealthDiagnostics.HealthEndpoint);
            app.MapHealthChecks(HealthDiagnostics.LivenessEndpoint, new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains(HealthDiagnostics.LivenessTag)
            });
            return app;
        }
        #endregion

        #region Private Helpers
        private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
        {
            var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration[Telemetry.OtlpEndpointEnvVar]);
            if (useOtlpExporter)
            {
                builder.Services.AddOpenTelemetry().UseOtlpExporter();
            }
            return builder;
        }
        #endregion
    }
}
