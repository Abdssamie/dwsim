using ServiceStack;

namespace DWSIM.WebApi.ServiceModel;

[Authenticate]
[Route("/sessions", "POST")]
public sealed class CreateSession : IReturn<CreateSessionResponse>
{
}

public sealed class CreateSessionResponse
{
    public string? SessionId { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

[Authenticate]
[Route("/sessions/{SessionId}/solve", "POST")]
public sealed class QueueSolve : IReturn<QueueSolveResponse>
{
    public required string SessionId { get; set; }
}

public sealed class QueueSolveResponse
{
    public string? JobId { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

public sealed class ExecuteSolveSession : IReturn<ExecuteSolveSessionResponse>
{
    public required string SessionId { get; set; }
    public string? UserAuthId { get; set; }
}

public sealed class ExecuteSolveSessionResponse
{
    public string? SessionId { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}
