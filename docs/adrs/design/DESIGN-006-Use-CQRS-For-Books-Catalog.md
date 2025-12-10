# Use CQRS For the Books Catalog Services

Date: 10 Dec 2025

Author: Mandar Dharmadhikari

## Status

Accepted

## Context

The Books Catalog system will serve as a core component of a social app, where book discovery and metadata lookup must be fast, searchable, and always available to readers across mobile and web clients. At the same time, the administration team will frequently perform write-heavy operations, including:
Curating and updating book/author metadata, Bulk uploads through CSV, Running periodic background ingestion jobs. Enforcing domain rules such as “books must have an author” and “authors with books cannot be deleted”

These business workflows generate two fundamentally different workloads:

### Reader-Facing Workload (High-volume reads)

* Search by author, genre, keywords, year
* Free text search across multiple fields
* Browsing lists, pagination, recommendations
* Traffic spikes from front-end apps

This demands:

* Low-latency responses
* Highly optimized read models
* Scalable infrastructure
* Denormalized data for search patterns

### Admin & Background Workload (Write-heavy + complex constraints)

* Bulk CSV ingestion
* Writes that must enforce domain invariants
* Occasional heavy operations (weekly summaries, imports)
* Event publishing to downstream systems (RabbitMQ consumers)

This demands:

* Strong consistency for domain rules
* Transaction safety
* Isolation from read performance hotspots
* Ability to run background jobs without impacting the reader experience

As the product grows, reader traffic and admin workflows will scale in different directions.

* A single CRUD system may cause unavoidable problems:
* Admin operations (e.g., CSV import) risk slowing down reader-facing queries
* Complex read queries (search, filtering) require denormalization, which pollutes the write model
* Strong consistency requirements on writes interfere with read-side performance tuning
* Read scaling and write scaling differ substantially; tying them together forces overprovisioning
* Background jobs introduce heavy load that can block user-facing reads

Since both the workloads have a different set of requirements, we need to find out a way to effectively scale them.

## Options Considered

### Single CRUD Model (Unified Read/Write)


#### Pros

* Simple to implement
* No infrastructure overhead
* Strong immediate consistency

#### Cons

* Poor separation: admin load and reader load interfere
* Read queries become slow or force schema compromises
* Difficult to scale independently
* High risk that CSV imports or background jobs affect user-facing operations

### Full CQRS + Event Sourcing

#### Pros

* Strong audit history
* Maximum flexibility for projections

#### Cons

* Risk of overengineering for the current domain
* Operational complexity (event stores, replays)

### Split Read/Write Models (CQRS without Event Sourcing)

#### Pros

* Clean domain-driven write model
* Fast denormalized read model
* Independent scalability
* Background jobs and CSV imports don’t impact readers
* Natural fit for Dapr pub/sub and outbox pattern

#### Cons

* Eventual consistency
* Multiple services and infrastructure components
* Requires projection consumers

## Decision

We will use CQRS with separate read and write models (without event sourcing).

* The write service enforces domain rules and emits events using an outbox → Dapr → RabbitMQ pipeline.
* The read service maintains denormalized projections optimized for search and filtering.

## Consequences

### Postives

* Reader-facing performance meets business SLA expectations
* Write service remains consistent and invariant-driven
* Admin workloads and background jobs no longer degrade reader experience
* Scaling strategy becomes more cost-efficient
* Search and filtering become significantly easier to optimize

### Negatives

* Eventual consistency between write and read
* More complexity in deployments and monitoring
* Data duplication in read projections
* Additional infrastructure to maintain
