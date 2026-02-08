# Hexagonal Architecture

Hexagonal architecture (Ports and Adapters) keeps domain logic in the center and isolates infrastructure behind ports.

## Why use it

- Strong testability for domain use cases.
- Infrastructure replacement with minimal domain impact.
- Explicit boundaries around side effects.

## When it is strong

- Domain complexity is high.
- Multiple delivery channels or adapters exist.
- Frequent infrastructure changes are expected.

## Risks

- Additional abstractions can slow small teams.
- Poorly designed ports become generic wrappers.
- Requires discipline to keep adapters thin.

## Structure Diagram

```mermaid
flowchart LR
    UI[Driving Adapter: API] --> IN[Input Port]
    IN --> D[Domain Use Case]
    D --> OUT1[Output Port: Repository]
    D --> OUT2[Output Port: Notification]
    OUT1 --> DB[(Database Adapter)]
    OUT2 --> MSG[Messaging Adapter]
```

## Runtime Flow

```mermaid
sequenceDiagram
    participant API as Driving Adapter
    participant UC as TransferFundsUseCase
    participant Repo as RepositoryPort
    participant Notif as NotificationPort

    API->>UC: transfer(from, to, amount)
    UC->>Repo: load accounts
    UC->>Repo: save new balances
    UC->>Notif: send transfer notification
    UC-->>API: success result
```

## Implementations

- C#: [`examples/csharp/HexagonalArchitectureDemo`](../../examples/csharp/HexagonalArchitectureDemo)
- Java: [`examples/java/hexagonal-architecture`](../../examples/java/hexagonal-architecture)
- C++: [`examples/cpp/hexagonal-architecture`](../../examples/cpp/hexagonal-architecture)
