using ServiceStack;
using System.Text.Json;

namespace DWSIM.WebApi.ServiceModel;

[Route("/sessions", "POST")]
public sealed class CreateSession : IReturn<CreateSessionResponse>
{
    public string? FilePath { get; set; }
}

public sealed class CreateSessionResponse
{
    public string? SessionId { get; set; }
    public SessionSummary? Session { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

[Route("/sessions/{SessionId}/load", "POST")]
public sealed class LoadSimulation : IReturn<SessionStateResponse>
{
    public required string SessionId { get; set; }
    public required string FilePath { get; set; }
}

[Route("/sessions/{SessionId}/state", "POST")]
public sealed class UpdateState : IReturn<UpdateStateResponse>
{
    public required string SessionId { get; set; }
    public List<SimulationCommand> Commands { get; set; } = [];
}

public sealed class SimulationCommand
{
    public required string Action { get; set; }
    public string? Tag { get; set; }
    public string? PropertyId { get; set; }
    public JsonElement? Value { get; set; }
    public string? ObjectType { get; set; }
    public string? Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public sealed class UpdateStateResponse
{
    public string? SessionId { get; set; }
    public List<CommandExecutionResult> Results { get; set; } = [];
    public SessionSummary? Session { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

public sealed class CommandExecutionResult
{
    public required string Action { get; set; }
    public bool Success { get; set; }
    public string? Tag { get; set; }
    public string? PropertyId { get; set; }
    public object? Value { get; set; }
    public string? Error { get; set; }
}

[Route("/sessions/{SessionId}/metadata", "GET")]
public sealed class GetMetadata : IReturn<GetMetadataResponse>
{
    public required string SessionId { get; set; }
    public string? Tag { get; set; }
}

public sealed class GetMetadataResponse
{
    public string? SessionId { get; set; }
    public List<ObjectMetadata> Objects { get; set; } = [];
    public List<string> AvailableObjectTypes { get; set; } = [];
    public ResponseStatus? ResponseStatus { get; set; }
}

public sealed class ObjectMetadata
{
    public required string Name { get; set; }
    public string? Tag { get; set; }
    public string? DisplayName { get; set; }
    public List<PropertyMetadata> Properties { get; set; } = [];
}

public sealed class PropertyMetadata
{
    public required string Id { get; set; }
    public string? Units { get; set; }
    public string? Description { get; set; }
}

[Route("/sessions/{SessionId}/solve", "POST")]
public sealed class SolveSimulation : IReturn<SolveSimulationResponse>
{
    public required string SessionId { get; set; }
    public int? TimeoutSeconds { get; set; }
}

public sealed class SolveSimulationResponse
{
    public string? SessionId { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = [];
    public SessionSummary? Session { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

[Route("/sessions/{SessionId}/logs", "GET")]
public sealed class GetSessionLogs : IReturn<GetSessionLogsResponse>
{
    public required string SessionId { get; set; }
}

public sealed class GetSessionLogsResponse
{
    public string? SessionId { get; set; }
    public List<SessionLogEntry> Logs { get; set; } = [];
    public ResponseStatus? ResponseStatus { get; set; }
}

public sealed class SessionStateResponse
{
    public string? SessionId { get; set; }
    public SessionSummary? Session { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

public sealed class SessionSummary
{
    public required string SessionId { get; set; }
    public string? FilePath { get; set; }
    public bool Solved { get; set; }
    public int ObjectCount { get; set; }
    public int PropertyPackageCount { get; set; }
    public int CompoundCount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
}

public sealed class SessionLogEntry
{
    public DateTime TimestampUtc { get; set; }
    public required string Type { get; set; }
    public required string Message { get; set; }
}
