# Vantage.AppHost

**Architectural Boundary:** Local Control Plane & Orchestration

## Purpose
This project replaces traditional `docker-compose.yml` files using .NET Aspire 13+ (NuGet-only model). It dynamically provisions and networks our local dependencies to ensure a zero-friction "F5" developer experience.

## Orchestrated Infrastructure
- **SQL Server:** Relational state (`sql`) mapped to volume `vantage-sql-data`.
- **Redis:** Distributed caching and SignalR backplane (`redis`) mapped to volume `vantage-redis-data`.
- **RabbitMQ:** MassTransit message broker (`messaging`) mapped to volume `vantage-rmq-data`.

## Strict Constraints
- **BANNED:** The `<IsAspireHost>true</IsAspireHost>` property is strictly prohibited to avoid deprecated workload dependencies.
- **No Domain Logic:** This project only wires up infrastructure; it does not process data.