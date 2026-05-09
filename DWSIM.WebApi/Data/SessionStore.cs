using System.Collections.Concurrent;
using DWSIM.Automation;
using DWSIM.Interfaces;
using DWSIM.WebApi.ServiceModel;
using ServiceStack;

namespace DWSIM.WebApi.Data;

public sealed class SessionStore(AutomationInterface automation)
{
    private readonly ConcurrentDictionary<string, SessionRecord> sessions = new();

    public SessionRecord CreateSession(string? filePath = null)
    {
        var sessionId = Guid.NewGuid().ToString("N");
        var session = new SessionRecord(sessionId, DateTime.UtcNow);
        sessions[sessionId] = session;

        if (string.IsNullOrWhiteSpace(filePath))
        {
            var flowsheet = automation.CreateFlowsheet();
            AttachLogging(session, flowsheet);
            session.SetFlowsheet(flowsheet, null);
        }
        else
        {
            LoadSimulation(sessionId, filePath);
        }

        return session;
    }

    public SessionRecord LoadSimulation(string sessionId, string filePath)
    {
        var session = GetSession(sessionId);
        if (!File.Exists(filePath))
        {
            throw HttpError.NotFound($"Simulation file was not found: {filePath}");
        }

        lock (session.SyncRoot)
        {
            var flowsheet = automation.LoadFlowsheet(filePath);
            AttachLogging(session, flowsheet);
            session.SetFlowsheet(flowsheet, filePath);
            session.Touch();
            return session;
        }
    }

    public SessionRecord GetSession(string sessionId)
    {
        if (!sessions.TryGetValue(sessionId, out var session))
        {
            throw HttpError.NotFound("Session was not found.");
        }

        session.Touch();
        return session;
    }

    public List<Exception> Solve(SessionRecord session, int? timeoutSeconds)
    {
        lock (session.SyncRoot)
        {
            return timeoutSeconds is > 0
                ? automation.CalculateFlowsheet3(session.Flowsheet, timeoutSeconds.Value)
                : automation.CalculateFlowsheet2(session.Flowsheet);
        }
    }

    private static void AttachLogging(SessionRecord session, IFlowsheet flowsheet)
    {
        flowsheet.SetMessageListener((message, type) => session.AddLog(type.ToString(), message));
    }
}

public sealed class SessionRecord
{
    private readonly List<SessionLogEntry> logs = [];

    public SessionRecord(string sessionId, DateTime createdAtUtc)
    {
        SessionId = sessionId;
        CreatedAtUtc = createdAtUtc;
        LastAccessedAtUtc = createdAtUtc;
    }

    public string SessionId { get; }
    public DateTime CreatedAtUtc { get; }
    public DateTime LastAccessedAtUtc { get; private set; }
    public string? FilePath { get; private set; }
    public IFlowsheet Flowsheet { get; private set; } = null!;
    public object SyncRoot { get; } = new();

    public void SetFlowsheet(IFlowsheet flowsheet, string? filePath)
    {
        Flowsheet = flowsheet;
        FilePath = filePath;
    }

    public void Touch() => LastAccessedAtUtc = DateTime.UtcNow;

    public void AddLog(string type, string message)
    {
        lock (logs)
        {
            logs.Add(new SessionLogEntry
            {
                TimestampUtc = DateTime.UtcNow,
                Type = type,
                Message = message,
            });
        }
    }

    public List<SessionLogEntry> GetLogs()
    {
        lock (logs)
        {
            return [.. logs];
        }
    }

    public SessionSummary ToSummary()
    {
        return new SessionSummary
        {
            SessionId = SessionId,
            FilePath = FilePath,
            Solved = Flowsheet.Solved,
            ObjectCount = Flowsheet.SimulationObjects.Count,
            PropertyPackageCount = Flowsheet.PropertyPackages.Count,
            CompoundCount = Flowsheet.SelectedCompounds.Count,
            CreatedAtUtc = CreatedAtUtc,
            LastAccessedAtUtc = LastAccessedAtUtc,
        };
    }
}
