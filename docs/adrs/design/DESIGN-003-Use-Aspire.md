# DESIGN-003-Use-Aspire-For-Local-Orchestration

Date: 6 Deember 2025
Author: Mandar Dharmadhikari

## Status

Accepted

## Context

In a microservices architecture, developers must orchestrate:

1. Multiple backend services
2. Databases (PostgreSQL, Redis)
3. Message brokers (RabbitMQ/Kafka)
4. Search engines (Elasticsearch)
5. Identity providers (OIDC stub)
6. UI frontends
7. Observability stack (OpenTelemetry, dashboards)

Managing these locally becomes difficult and error-prone. We need to finalize a solution which will make the life of the developers easy.

## Options Considered

1. **Aspire**: Provides out of box service discovery, observability, polyglot support, rich support using Nuget packages etc.
2. **Docker Compose**: Workable but not scalable for many services. Reqires explicit wiring for each service

## Decision

We will use .NET Aspire as the orchestration layer for local development, testing, and pre-prod environments.

1. All services will participate in Aspire for:
2. Local environment wiring
3. Configuration of connection strings
4. Launching dependent containers
5. Distributed tracing visualization
6. Managing event bus wiring
7. Developer productivity tooling

## Consequences

### Positives

1. Fast onboarding of developers
2. Consistent and repeatable local environment
3. Great visibility of microservice interactions
4. Simplified configuration & secrets management
5. Strong integration with .NET ecosystem

### Negatives

1. Aspire is still evolving — occasional breaking changes
2. Some non-.NET services might need manual integration
3. Production deployments still need Helm charts or Terraform
