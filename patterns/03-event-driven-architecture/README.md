# Event-Driven Architecture

Event-driven architecture exchanges facts (events) through a broker/topic so producers and consumers are loosely coupled.

## Why use it

- Handles bursts with asynchronous buffering.
- Supports independent consumers for the same event.
- Enables reactive workflows.

## When it is strong

- Integrations with many downstream systems.
- Non-blocking side effects (email, analytics, fulfillment).
- Need to fan out one action into many reactions.

## Risks

- Eventual consistency surprises.
- Hard end-to-end tracing.
- Schema evolution and duplicate delivery handling.

## Structure Diagram

```mermaid
flowchart LR
    P[Publisher] --> T[(Topic)]
    T --> C1[Consumer A]
    T --> C2[Consumer B]
    T --> C3[Consumer C]
```

## Runtime Flow

```mermaid
sequenceDiagram
    participant API as Order API
    participant Bus as Event Bus
    participant Inv as Inventory Handler
    participant Notif as Notification Handler

    API->>Bus: OrderPlaced
    Bus-->>Inv: OrderPlaced
    Bus-->>Notif: OrderPlaced
    Inv->>Bus: InventoryReserved
    Bus-->>Notif: InventoryReserved
```

## Implementations

- C#: [`examples/csharp/EventDrivenArchitectureDemo`](../../examples/csharp/EventDrivenArchitectureDemo)
- Java: [`examples/java/event-driven-architecture`](../../examples/java/event-driven-architecture)
- C++: [`examples/cpp/event-driven-architecture`](../../examples/cpp/event-driven-architecture)
