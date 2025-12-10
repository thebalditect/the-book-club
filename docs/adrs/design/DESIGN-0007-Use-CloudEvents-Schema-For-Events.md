# DESIGN-0007-Use-CloudEvents-Schema-For-Events

Date: 10 December 2025

Author: Mandar Dharmadhikari

## Status

Accepted

## Related ADRs

1. [DESIGN-001-Use-Microservices](/docs/adrs/design/DESIGN-001-Use-Microservices.md)
2. [DESIGN-003-Use-Aspire](/docs/adrs/design/DESIGN-003-Use-Aspire.md)


## Context

Our system emits domain events from the write side and distributes them through an outbox → Dapr → RabbitMQ pipeline.

Multiple system components rely on these events:

* Read model projectors
* Background processors
* Other internal services and automations
* Potential future consumers subscribing through pub/sub

Without a standardized event envelope, each publisher could introduce slight variations in fields such as IDs, timestamps, types, correlation metadata, and payload formats. This leads to:

* Schema drift
* Inconsistent metadata
* Harder validation
* Increased consumer complexity
* Lack of interoperability across components
* Higher friction when switching or adding transport layers

We need a consistent, well-defined, platform-neutral event schema that works cleanly across Dapr and RabbitMQ, supports versioning, and remains stable over time.

[CloudEvents](https://cloudevents.io/) provides a standardized envelope that solves these problems with minimal overhead.

## Options Considered

### Custom JSON Event Format

#### Pros

* Maximum flexibility
* Very simple initial implementation

#### Cons

* No standardization across services
* Requires reinventing metadata conventions (id, type, time, trace info)
* Lacks compatibility with existing tooling
* Schema drift likely over time
* More effort to validate and evolve consistently

### CloudEvents v1.0

#### Pros

* CNCF standard, stable, widely supported
* Aligns cleanly with Dapr (Dapr already wraps messages as CloudEvents)
* Standardized metadata: id, source, specversion, type, subject, time
* Reduces custom validation and schema drift
* Compatible with many brokers and HTTP transports
* Improves observability and correlation tracing
* Version-safe event evolution

#### Cons

* Slightly more verbose message envelope
* Development teams must follow the specification and validation rules

## Decision

We will adopt CloudEvents v1.0 as the default and required event schema for all published events in the system. All events should include the standard CloudEvents required attributes.

## Consequences

### Positives

* Consistent event structure across all publishers and consumers
* Strong interoperability across messaging systems
* Easy validation, schema governance, and event evolution
* Compatibility with existing libraries and tooling
* Better traceability and observability (standard fields for IDs, timestamps)
* Simplifies cross-service contracts and reduces consumer complexity

### Negatives

* Slight learning curve for developers unfamiliar with CloudEvents
* Slight increase in payload overhead
* Requires standard tooling for serialization/deserialization

## Compliance

Each team will be responsible for audting the adoption and this will be evaluated periodically in the wider Architecture Review Board meetings

## Related Links

The detailed specification for the cloud events schema can be found at [Cloud Events Spec 1.0.4](https://github.com/cloudevents/spec/blob/v1.0.2/cloudevents/spec.md)
