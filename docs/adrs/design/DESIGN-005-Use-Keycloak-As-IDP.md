# DESIGN-005-Use-Keycloak-As-Identity Provider

Date: 6 December 2025
Author: Mandar Dharmadhikari

## Status

Accepted

## Context

The Book Club is designed as a microservice-based system using .NET 10, Aspire, and Dapr.

A central requirement is:

1. A single identity provider for all authentication (frontend, backend, service-to-service).
2. A unified RBAC model for all users (readers, moderators, admins).
3. Support for OAuth 2.0 + OpenID Connect flows for web apps, mobile clients, admin tools, and services.
4. A way to centralize user management (registration, password resets, MFA, social logins).
5. Ability to extend with custom user attributes, group membership, and token mappers.
6. Support for session management, token revocation, and account disabling.
7. Works with microservices, API gateway, and service mesh patterns.
8. Can run locally with Aspire/Docker and scale to Kubernetes in production.

The platform needs a robust, enterprise-ready IAM solution that:

1. Integrates well with .NET & Dapr
2. Supports long-term growth
3. Does not force custom authorization logic inside microservices

Identity must be centralized, standardized, and externally managed — not home-grown.

## Options Considered

1. **Keycloak**: Self-hosted identity provider supporting OAuth2/OIDC, RBAC, SSO, MFA.
2. **Duende Identity Server**: .Net native and support for ASP.NET core, commerical license does not provide inbuilt Admin UI
3. **Custom Solution**: A custom implementation

## Decision

We will adopt Keycloak as the platform-wide Identity and Access Management (IAM) solution for:

1. Authentication (OIDC/OAuth2.0)
2. Authorization / RBAC (roles & permissions)
3. User & group management
4. Token management (access tokens, refresh tokens, introspection)
5. Service accounts for backend services
6. Federation with external providers (optional)

## Consequences

### Positives

1. All user roles, permissions, and policies are managed in Keycloak. Microservices remain stateless and clean
2. Frontend, Gateway, internal services all use modern authentication without custom code
3. Allows user management, role assignment, password management, MFA configuration
4. OpenID, OAuth2, SAML, LDAP support provides long-term flexibility
5. Single Keycloak container easily starts with Docker
6. Short-lived access tokens, long-lived refresh tokens, revocation, introspection — all handled by Keycloak
7. Services only need to validate JWTs; no internal session logic

### Negatives

1. Operational overhead to maintain the backend databases, clusters, certificates etc.
2. Steep learning curve to understand how keycloak works
