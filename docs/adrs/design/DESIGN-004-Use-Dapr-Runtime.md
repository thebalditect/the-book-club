# DESIGN-004-Use-Dapr-Runtime

Date: 6 December 2025

Author: Mandar Dharmadhikari

## Status

Accepted

## Related ADRs

1. [DESIGN-001-Use-Microservices](/docs/adrs/design/DESIGN-001-Use-Microservices.md)
2. [DESIGN-003-Use-Aspire](/docs/adrs/design/DESIGN-003-Use-Aspire.md)

## Context

The Book Club consists of multiple autonomous bounded contexts implemented as microservices. These services require a common set of distributed system building blocks, such as:

1. Service-to-service communication with resiliency
2. Pub/Sub messaging for domain events
3. State management (for caching, ephemeral workflows, coordination)
4. Bindings to external systems (email, queues, storage)
5. Secret management
6. Distributed workflows
7. Sidecar patterns like retries, rate limiting, timeouts

Using custom or service-specific implementations increases:

1. Boilerplate
2. inconsistency across services
3. Operational overhead
4. Risk of violating architectural principles
5. Duplication of distributed systems logic

We need a strong community backed/ proprietry solution which will help us stream line common side tasks.

## Decision

We will adopt Dapr Runtime as a standardized cross-cutting microservices building block for The Book Club platform. Dapr (Distributed Application Runtime) provides an open-source, CNCF-backed sidecar runtime that encapsulates these patterns in a consistent way across languages and platforms.

Dapr will be used for:

1. Service invocation (gRPC/HTTP) with built-in resiliency
2. Pub/Sub for domain events
3. State stores (Redis/Postgres/etc.) for fast or ephemeral state
4. Input/Output bindings
5. Secret stores
6. Observability metadata

Each microservice will integrate the Dapr SDK for .NET and include the Dapr sidecar in local development (via Aspire) and in production deployments.

In case a different tech stack is used for a particular service, in that case we will evaluate the availability of Dapr runtime for the tech stack.

## Consequences

### Postives

1. Standardized, battle-tested distributed primitives
2. Reduced boilerplate in microservices
3. Easier cross-language/technology integration
4. Unified approach to Pub/Sub, state, secrets
5. Strong synergy with Aspire for local orchestration
6. Vendor-neutral infrastructure
7. Easier evolution of architecture over time
8. Simplified testing (replace backends with memory stores during integration tests)

## Negatives

1. Additional operational component (sidecars) → mitigated by container orchestration (Kubernetes, Aspire locally).
2. Learning curve for developers unfamiliar with Dapr APIs.
3. Abstractions may limit fine-grained cloud-specific tuning (rare but possible).
4. Observability, logging, and configuration complexity slightly increase.
5. Some operational scenarios require Dapr-specific configuration (component manifests, sidecar policies).
