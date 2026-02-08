# Main Architecture Pattern Playbook

This repository provides a practical, code-first guide for 6 core architecture patterns:

1. Layered Architecture
2. Microservice Architecture
3. Event-Driven Architecture
4. Client-Server Architecture
5. Plugin-Based Architecture
6. Hexagonal Architecture

The primary implementation language is **C#**. Extra implementations are included in **Java** and **C++** for cross-language comparison.

## Repository Layout

```text
Main_Architecture_Patern/
  docs/
    comparison.md
  patterns/
    01-layered-architecture/
    02-microservice-architecture/
    03-event-driven-architecture/
    04-client-server-architecture/
    05-plugin-based-architecture/
    06-hexagonal-architecture/
  examples/
    csharp/
      LayeredArchitectureDemo/
      MicroserviceArchitectureDemo/
      EventDrivenArchitectureDemo/
      ClientServerArchitectureDemo/
      PluginBasedArchitectureDemo/
      HexagonalArchitectureDemo/
    java/
      client-server/
    cpp/
      plugin-based/
```

## Quick Navigation

- Full comparison: `docs/comparison.md`
- Layered: `patterns/01-layered-architecture/README.md`
- Microservice: `patterns/02-microservice-architecture/README.md`
- Event-Driven: `patterns/03-event-driven-architecture/README.md`
- Client-Server: `patterns/04-client-server-architecture/README.md`
- Plugin-Based: `patterns/05-plugin-based-architecture/README.md`
- Hexagonal: `patterns/06-hexagonal-architecture/README.md`

## Language Focus

- **C#**: Full set of 6 runnable demos.
- **Java**: Client-server demo for JVM style network architecture.
- **C++**: Plugin-based demo showing low-level extensibility model.

## Build and Run

### C# demos

```powershell
cd Main_Architecture_Patern/examples/csharp/LayeredArchitectureDemo

dotnet run
```

Repeat for each C# demo folder.

### Java demo

```powershell
cd Main_Architecture_Patern/examples/java/client-server

javac SimpleServer.java SimpleClient.java
java SimpleServer
# in another terminal
java SimpleClient
```

### C++ demo

Compiler availability depends on your local toolchain.

```powershell
cd Main_Architecture_Patern/examples/cpp/plugin-based
# Example (if g++ is installed)
g++ -std=c++17 -O2 main.cpp -o plugin_demo
./plugin_demo
```

## Decision Summary

- Start with **Layered** when domain and delivery speed matter more than scale distribution.
- Move to **Hexagonal** when testability and external dependency isolation become critical.
- Use **Microservices** when team autonomy and independent deployment outweigh operational cost.
- Choose **Event-Driven** for asynchronous workflows and loose coupling under burst load.
- Keep **Client-Server** for straightforward request/response systems.
- Add **Plugin-Based** architecture when product extensibility by third parties is a core requirement.

For detailed trade-offs, see `docs/comparison.md`.
