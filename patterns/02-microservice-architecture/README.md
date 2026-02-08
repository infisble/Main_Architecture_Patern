# Microservice Architecture

Microservice architecture splits a system into independently deployable services around business capabilities.

## Why use it

- Independent deployment per service.
- Better team autonomy.
- Service-level scaling and fault isolation.

## When it is strong

- Multiple teams ship in parallel.
- Different components have different scaling profiles.
- Need strict ownership boundaries.

## Risks

- Operational overhead (observability, platform, CI/CD).
- Distributed transactions and consistency issues.
- Increased debugging complexity across service boundaries.

## Structure Diagram

```mermaid
flowchart TD
    Client[Client] --> G[API Gateway]
    G --> U[User Service]
    G --> O[Order Service]
    U --> UDB[(User DB)]
    O --> ODB[(Order DB)]
```

## Runtime Flow

```mermaid
sequenceDiagram
    participant C as Client
    participant G as API Gateway
    participant U as UserService
    participant O as OrderService

    C->>G: Place order
    G->>U: Validate user
    U-->>G: User valid
    G->>O: Create order
    O-->>G: Order created
    G-->>C: 201 Created
```

## Implementations

- C#: [`examples/csharp/MicroserviceArchitectureDemo`](../../examples/csharp/MicroserviceArchitectureDemo)
- Java: [`examples/java/microservice-architecture`](../../examples/java/microservice-architecture)
- C++: [`examples/cpp/microservice-architecture`](../../examples/cpp/microservice-architecture)
