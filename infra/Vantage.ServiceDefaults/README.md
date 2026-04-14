# Vantage.ServiceDefaults

**Architectural Boundary:** Observability & Resilience Baseline

## Purpose
This project guarantees that every microservice in the Project Vantage ecosystem is instrumented identically. By referencing this project, a microservice automatically inherits our production-grade telemetry and health standards.

## Capabilities
- **OpenTelemetry (v1.12.0+):** Standardized distributed tracing, metrics, and OTLP exportation.
- **Health Diagnostics:** Unified `/health` and `/alive` endpoints.
- **Resilience:** Default HTTP retry and circuit breaker policies via `Microsoft.Extensions.Http.Resilience`.
- **Service Discovery:** Abstracted endpoint resolution for Aspire integration.

## Strict Constraints
- Must be referenced by every executable project in the `/services` and `/gateways` directories.