# Vantage.Shared

**Architectural Boundary:** Core Domain Kernel / Anti-Corruption Layer

## Purpose
This project is the foundational bedrock of the Project Vantage ecosystem. It is strictly limited to cross-cutting primitives that must be universally understood by all microservices.

## Strict Constraints
- **NO Business Logic:** This project must never contain rules related to matchmaking, users, or games.
- **NO Database Dependencies:** Do not reference Entity Framework or Dapper here.
- **RFC 9457 Enforcement:** All domain exceptions must inherit from `VantageException` to guarantee standardized HTTP routing in the API layer.