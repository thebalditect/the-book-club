# DESIGN-001-Use-Microservices

Date: 6 December 2025

Status: Accepted

## Context

The Book Club is a social media platform centered around books. It includes:

1. complex reader identity and relationships
2. book posts, reviews, quotes, reactions
3. book clubs, reading challenges, group discussions
4. search, recommendations, analytics
5. moderation, notifications, and content policies

The domain spans multiple bounded contexts aligned with DDD. The system must support:

1. independent team ownership
2. scalable modules (e.g., feed service, club service, catalog service)
3. high read/write throughput
4. extensibility as community features evolve
5. ability to plug in new recommendation algorithms
6. continuous deployment without disrupting the ecosystem

A monolith would limit scalability and team autonomy.

## Options Considered

1. **Microservice Architecture**: Independent scalable services and team autonomy
2. **Modular Monolith Architecture**: Vertical sliced monolith per Bounded Context, shared codebase

## Decision

We adopt a microservices architecture aligned with DDD bounded contexts.

Bounded contexts such as ReaderProfile, BookExperience, BookCommunity, BookCatalog, Quotes, RecommendationEngine etc. will be deployed as independent services with clear APIs, events, and ownership boundaries.

Each service will have:

1. Its own domain model
2. Its own persistence store (polyglot allowed)
3. Its own CI/CD lifecycle
4. Integration via REST + asynchronous domain events (RabbitMQ/Kafka depending on environment)

## Consequences

### Positives

1. Strong alignment with DDD
2. Independent deployability
3. Improved performance scaling
4. Better fault isolation
5. Allows experimentation within services

## Negatives

1. Increased operational complexity
2. Need for distributed monitoring & tracing
3. Eventual consistency inherent in distributed systems
4. Requires API gateway and service mesh as we grow
5. Code can devolve and decay into a distributed mess if not regulated and audited regularly
