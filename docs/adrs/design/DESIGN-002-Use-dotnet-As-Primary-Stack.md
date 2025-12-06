# DESIGN-002-Use-dotnet-As-Primary-Tech-Stack

Date: 6 December 2025,
Status: Accepted

## Context

The Book Club requires:

1. High-performance backend supporting real-time interactions
2. Strong tooling for enterprise-level observability
3. Excellent async IO and concurrency (feeds, notifications)
4. Robust ecosystem for microservices and cloud deployments
5. Strong DDD tooling, patterns, and libraries
6. Long-term maintainability and developer productivity

We need to identify and adopt a primary tech stack for buildig the book club as per identified requirements.

### Options Considered

1. **Dotnet**: Good tooling and strong community support. High performance and closely aligns with DDD
2. **Golang**: Strong performance but lacks OOP/DDD expressiveness.
3. **Node JS**: Fast iteration but weak domain modeling.

## Decision

 We will use .Net to build most of the backend for The Book Club. Different tech stack will be considered as an exception and will be documented using proper decision records

### Rationale

#### Mature ecosystem for DDD

1. Aggregates, Value Objects, Repositories, CQRS, Eventing
2. Strong patterns via community and Microsoft guidance

#### Performance and reliability

1. Very high throughput compared to dynamic languages
2. Great for high-load event-driven systems

#### Modern developer experience

1. Minimal APIs
2. Native async/await
3. High-quality tooling (Rider, VSCode, Visual Studio)

#### Native support for cloud-native features

1. gRPC, Async messaging, Orleans (if needed later)
2. Observability via OpenTelemetry

#### Ease of hiring

1. Large community, companies comfortable with .NET
2. Best long-term maintainability profile

#### Compatibility with AspNetCore + Aspire

Excellent developer experience for local orchestration

## Consequences

### Postive

1. Strong alignment with DDD patterns
2. High performance and reliability
3. Mature async and concurrency support
4. Stable long-term evolution
5. Faster development with minimal ceremony

### Negative

1. Slightly heavier memory footprint than Golang
2. ML and data tooling better supported in Python (but can integrate via microservices as exception)
