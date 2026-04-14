# Project Vantage

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C# 12](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)
![.NET Aspire](https://img.shields.io/badge/Aspire-13+-8A2BE2)
![CI Status](https://img.shields.io/github/actions/workflow/status/zPagano/ProjectVantage/ci.yml?branch=main)

Project Vantage is a highly scalable, game-agnostic commercial esports ecosystem built on .NET 10 and C# 12. 

## Architectural Blueprint
- **Topology:** Regional DNS-level Sharding. Strictly microservices.
- **Orchestration:** .NET Aspire 13+ (NuGet-only model).
- **Ingress:** YARP (Yet Another Reverse Proxy) Gateway.
- **Data Access:** SQL Server (CQRS via EF Core & Dapper) & Redis.
- **Messaging:** MassTransit (RabbitMQ).
- **Observability:** OpenTelemetry & RFC 9457 Problem Details.

## Repository Structure & Bounded Contexts

```mermaid
graph TD
    Root[ProjectVantage Repository] --> Core[/core/]
    Root --> Infra[/infra/]
    Root --> Services[/services/]
    Root --> Gateways[/gateways/]
    
    Core --> Shared[Vantage.Shared]
    Infra --> AppHost[Vantage.AppHost]
    Infra --> SD[Vantage.ServiceDefaults]
    
    style Root fill:#2d3436,stroke:#dfe6e9,stroke-width:2px,color:#fff
    style Core fill:#0984e3,stroke:#74b9ff,color:#fff
    style Infra fill:#00b894,stroke:#55efc4,color:#fff
````

  - **[`/core/Vantage.Shared`](https://www.google.com/search?q=./core/Vantage.Shared/README.md)** - Foundational primitives and cross-cutting domain logic.
  - **[`/infra/Vantage.AppHost`](https://www.google.com/search?q=./infra/Vantage.AppHost/README.md)** - Control plane, Docker orchestration, and service defaults.
  - **[`/infra/Vantage.ServiceDefaults`](https://www.google.com/search?q=./infra/Vantage.ServiceDefaults/README.md)** - Observability and resilience baseline.
  - **`/services/`** - Bounded business logic contexts (Matchmaking, Social, etc.).
  - **`/gateways/`** - Ingress controllers and Anti-Corruption Layers (ACL).

## Local Developer Experience (The "F5" Experience)

This project enforces a zero-cost local developer experience using Docker and .NET Aspire.

### Prerequisites

  - Windows 11
  - Visual Studio 2026
  - .NET 10 SDK
  - PowerShell 7
  - Docker Desktop (Must be running)

### Startup Instructions

1.  Clone the repository.
2.  Open `ProjectVantage.slnx` in Visual Studio.
3.  Ensure `Vantage.AppHost` is set as your **Startup Project**.
4.  Press **F5** to build the solution and launch the .NET Aspire Dashboard.
5.  Aspire will automatically provision and map the local SQL Server, Redis, and RabbitMQ Docker containers.