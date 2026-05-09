using DWSIM.WebApi.Data;
using DWSIM.WebApi.ServiceModel;
using ServiceStack;
using ServiceStack.Jobs;

namespace DWSIM.WebApi.Services;

public sealed class SessionServices(DwsimSessionStore sessions, IBackgroundJobs jobs) : Service
{
    public CreateSessionResponse Post(CreateSession request)
    {
        var sessionId = sessions.CreateSession(GetSession().UserAuthId);

        return new CreateSessionResponse
        {
            SessionId = sessionId,
        };
    }

    public async Task<QueueSolveResponse> Post(QueueSolve request)
    {
        sessions.AssertSessionExists(request.SessionId, GetSession().UserAuthId);

        var jobRef = jobs.EnqueueApi(new ExecuteSolveSession
        {
            SessionId = request.SessionId,
            UserAuthId = GetSession().UserAuthId,
        });

        return new QueueSolveResponse
        {
            JobId = jobRef.Id.ToString(),
        };
    }

    public object Any(ExecuteSolveSession request)
    {
        sessions.AssertSessionExists(request.SessionId, request.UserAuthId);

        return new ExecuteSolveSessionResponse
        {
            SessionId = request.SessionId,
        };
    }
}
