# DOMAIN-0001-Use-Pragmatic-Domain-Driven-Design

Date: 6 December 2025

Author: Mandar Dharmadhikari

## Status

Accepted

## Context

The Book Club is a social media app for book lovers. It is being built to provide a book centric social media experience to the book lovers. The components of the system are complex and will require us to have a clear understanding of the business domain

## Options Considered

1. **Domain Driven Design** : Digging deep into the business requirements and building a domain model for each domain
2. **CRUD Based Approach**: Desiging system based on the principles of create, read, update and delete of things in system
3. **Hybrid**: Combine DDD and CRUD approaches based on the complexity of the domain being handled

## Decision

We will use a hybrid approach for building the Book Club, system which require high performance will adopt more CRUD and minimal DDD patterns to avoid memory overload. We will document the decision for each domain separately.

## Consequences

### Postives

1. Freedom to choose best suited style according to domain complexity
2. Pragmatic DDD will allow us to preserve the knowledge of the domain while still being pragmatic

### Negatives

1. Detailed discussions required to flush out domain details and behaviors
2. Overhead to maintain decision per domain based on complexity
