namespace Vantage.ServiceDefaults
{
    internal static class HealthDiagnostics
    {
        public const string LivenessTag = "live";
        public const string SelfCheckName = "self";
        public const string HealthEndpoint = "/health";
        public const string LivenessEndpoint = "/alive";
    }

    internal static class Telemetry
    {
        public const string OtlpEndpointEnvVar = "OTEL_EXPORTER_OTLP_ENDPOINT";
    }
}
