# DWSIM Cloud API: Implementation Plan (ServiceStack Optimized)

This document outlines the architecture for exposing the DWSIM Headless Engine using **ServiceStack**. ServiceStack's message-based architecture is the ideal framework for our "Command-Driven" pattern, enabling a functional MVP in a single day.

## 1. Why ServiceStack for DWSIM?

- **Message-Based (Command Pattern)**: ServiceStack treats every API request as a DTO (Data Transfer Object), which maps 1:1 to our "Simulation Commands".
- **Instant UI (Locode)**: Provides an automatic management UI to view active sessions and test commands without writing frontend code.
- **Built-in Real-time (SSE)**: Built-in Server-Sent Events for streaming solver logs instantly.
- **Typed Clients**: Automatically generates C#, TypeScript, and Python clients to use the simulation engine from any platform.

---

## 2. Technical Architecture

### A. The "Generic Proxy" Service
ServiceStack services will handle "Actions" rather than "Resources".
- `SimulationState`: A Request DTO that carries a list of commands.
- `SimulationSolve`: A Request DTO that triggers the math engine.

### B. Session Isolation
Using ServiceStack's **Memory Cache** or **Redis** to track `IFlowsheet` session IDs.

---

## 3. One-Day MVP Implementation Roadmap

### Phase 1: ServiceStack Foundation (Morning)
- **Setup**: Initialize a ServiceStack "Web" project.
- **Dependencies**: Reference `DWSIM.Automation` and `DWSIM.Interfaces`.
- **Session Service**: Implement `CreateSession` DTO.
    - `POST /sessions` -> Returns `SessionID`.

### Phase 2: The Command Bridge (Early Afternoon)
- **Command DTO**:
  ```csharp
  public class UpdateState : IReturn<UpdateStateResponse> {
      public string SessionId { get; set; }
      public List<SimulationCommand> Commands { get; set; }
  }
  ```
- **Service Logic**: Map `SimulationCommand.Action` to `IFlowsheet` methods using a simple Registry or Reflection.
- **Instant Validation**: Use ServiceStack **Locode** to manually fire commands and see the flowsheet update.

### Phase 3: Solving & Real-time Logs (Late Afternoon)
- **Solver Service**: Implement `POST /sessions/{id}/solve`.
- **Logs via SSE**: Use ServiceStack's `IServerEvents` to push `ShowMessage` events from DWSIM to the client.
- **Metadata Discovery**: Implement a `GetMetadata` DTO that returns available property IDs for any tag.

---

## 4. Scaling & Maintainability
1. **Auto-Documentation**: ServiceStack's `/ui` gives a full Swagger-like interface for the simulation protocol.
2. **Type Safety**: The API is defined by C# DTOs, ensuring that the "Simulation Protocol" is always versioned and safe.
3. **Async Processing**: Leverage ServiceStack's `MQ` support to offload heavy calculations to background workers if needed.

## 5. Success Metrics (The 24-Hour Goal)
- **Connectivity**: Load a `.dwxmz` and change a temperature via a REST call.
- **Convergence**: Trigger a solve and get the converged results back as JSON.
- **Visibility**: View solver logs in real-time in a browser.
