using ServiceStack;
using ServiceStack.Caching;

namespace DWSIM.WebApi.Data;

public sealed class DwsimSessionStore(ICacheClient cache)
{
    private static readonly TimeSpan SessionTtl = TimeSpan.FromHours(8);

    public string CreateSession(string? userAuthId)
    {
        var sessionId = Guid.NewGuid().ToString("N");
        cache.Set(CacheKey(sessionId), new DwsimSessionRecord(sessionId, userAuthId), SessionTtl);
        return sessionId;
    }

    public void AssertSessionExists(string sessionId, string? userAuthId)
    {
        var session = cache.Get<DwsimSessionRecord>(CacheKey(sessionId));
        if (session is null || session.UserAuthId != userAuthId)
        {
            throw HttpError.NotFound("Session was not found.");
        }
    }

    private static string CacheKey(string sessionId) => $"dwsim:session:{sessionId}";
}

public sealed record DwsimSessionRecord(string SessionId, string? UserAuthId);

