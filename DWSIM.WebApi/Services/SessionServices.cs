using System.Text.Json;
using DWSIM.Interfaces;
using DWSIM.Interfaces.Enums;
using DWSIM.WebApi.Data;
using DWSIM.WebApi.ServiceModel;
using ServiceStack;

namespace DWSIM.WebApi.Services;

public sealed class SessionServices(SessionStore sessions) : Service
{
    public CreateSessionResponse Post(CreateSession request)
    {
        var session = sessions.CreateSession(request.FilePath);

        return new CreateSessionResponse
        {
            SessionId = session.SessionId,
            Session = session.ToSummary(),
        };
    }

    public SessionStateResponse Post(LoadSimulation request)
    {
        var session = sessions.LoadSimulation(request.SessionId, request.FilePath);

        return new SessionStateResponse
        {
            SessionId = session.SessionId,
            Session = session.ToSummary(),
        };
    }

    public UpdateStateResponse Post(UpdateState request)
    {
        var session = sessions.GetSession(request.SessionId);
        var results = new List<CommandExecutionResult>();

        lock (session.SyncRoot)
        {
            foreach (var command in request.Commands)
            {
                results.Add(ExecuteCommand(session.Flowsheet, command));
            }
        }

        return new UpdateStateResponse
        {
            SessionId = session.SessionId,
            Results = results,
            Session = session.ToSummary(),
        };
    }

    public GetMetadataResponse Get(GetMetadata request)
    {
        var session = sessions.GetSession(request.SessionId);

        lock (session.SyncRoot)
        {
            var objects = string.IsNullOrWhiteSpace(request.Tag)
                ? session.Flowsheet.SimulationObjects.Values.AsEnumerable()
                : [GetObject(session.Flowsheet, request.Tag)];

            return new GetMetadataResponse
            {
                SessionId = session.SessionId,
                AvailableObjectTypes = [.. session.Flowsheet.GetAvailableFlowsheetObjectTypeNames().Cast<object>().Select(x => x.ToString()!).Order()],
                Objects = [.. objects.Select(BuildObjectMetadata)],
            };
        }
    }

    public SolveSimulationResponse Post(SolveSimulation request)
    {
        var session = sessions.GetSession(request.SessionId);
        var errors = sessions.Solve(session, request.TimeoutSeconds);

        return new SolveSimulationResponse
        {
            SessionId = session.SessionId,
            Success = errors.Count == 0,
            Errors = [.. errors.Select(error => error.ToString())],
            Session = session.ToSummary(),
        };
    }

    public GetSessionLogsResponse Get(GetSessionLogs request)
    {
        var session = sessions.GetSession(request.SessionId);

        return new GetSessionLogsResponse
        {
            SessionId = session.SessionId,
            Logs = session.GetLogs(),
        };
    }

    private static CommandExecutionResult ExecuteCommand(IFlowsheet flowsheet, SimulationCommand command)
    {
        try
        {
            return command.Action.Trim().ToLowerInvariant() switch
            {
                "add-object" => AddObject(flowsheet, command),
                "set-property" => SetProperty(flowsheet, command),
                "get-property" => GetProperty(flowsheet, command),
                _ => new CommandExecutionResult
                {
                    Action = command.Action,
                    Success = false,
                    Error = $"Unsupported command action '{command.Action}'.",
                },
            };
        }
        catch (Exception ex)
        {
            return new CommandExecutionResult
            {
                Action = command.Action,
                Success = false,
                Tag = command.Tag,
                PropertyId = command.PropertyId,
                Error = ex.Message,
            };
        }
    }

    private static CommandExecutionResult AddObject(IFlowsheet flowsheet, SimulationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.ObjectType))
        {
            throw HttpError.BadRequest("ObjectType is required for add-object.");
        }

        if (string.IsNullOrWhiteSpace(command.Tag))
        {
            throw HttpError.BadRequest("Tag is required for add-object.");
        }

        var simulationObject = Enum.TryParse(command.ObjectType, ignoreCase: true, out DWSIM.Interfaces.Enums.GraphicObjects.ObjectType typedObject)
            ? flowsheet.AddObject(typedObject, command.X, command.Y, command.Id ?? Guid.NewGuid().ToString("N"), command.Tag)
            : flowsheet.AddFlowsheetObject(command.ObjectType, command.Tag);

        return new CommandExecutionResult
        {
            Action = command.Action,
            Success = true,
            Tag = simulationObject.GraphicObject?.Tag ?? simulationObject.Name,
            Value = simulationObject.Name,
        };
    }

    private static CommandExecutionResult SetProperty(IFlowsheet flowsheet, SimulationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.PropertyId))
        {
            throw HttpError.BadRequest("PropertyId is required for set-property.");
        }

        var simulationObject = GetObject(flowsheet, command.Tag);
        var value = ConvertJsonValue(command.Value);
        var success = simulationObject.SetPropertyValue(command.PropertyId, value);

        return new CommandExecutionResult
        {
            Action = command.Action,
            Success = success,
            Tag = simulationObject.GraphicObject?.Tag ?? simulationObject.Name,
            PropertyId = command.PropertyId,
            Value = simulationObject.GetPropertyValue(command.PropertyId),
        };
    }

    private static CommandExecutionResult GetProperty(IFlowsheet flowsheet, SimulationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.PropertyId))
        {
            throw HttpError.BadRequest("PropertyId is required for get-property.");
        }

        var simulationObject = GetObject(flowsheet, command.Tag);

        return new CommandExecutionResult
        {
            Action = command.Action,
            Success = true,
            Tag = simulationObject.GraphicObject?.Tag ?? simulationObject.Name,
            PropertyId = command.PropertyId,
            Value = simulationObject.GetPropertyValue(command.PropertyId),
        };
    }

    private static ISimulationObject GetObject(IFlowsheet flowsheet, string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw HttpError.BadRequest("Tag is required.");
        }

        var simulationObject = flowsheet.GetFlowsheetSimulationObject(tag)
            ?? flowsheet.GetObject(tag);

        if (simulationObject is null)
        {
            throw HttpError.NotFound($"Object '{tag}' was not found.");
        }

        return simulationObject;
    }

    private static ObjectMetadata BuildObjectMetadata(ISimulationObject simulationObject)
    {
        return new ObjectMetadata
        {
            Name = simulationObject.Name,
            Tag = simulationObject.GraphicObject?.Tag,
            DisplayName = simulationObject.GetDisplayName(),
            Properties =
            [
                .. simulationObject.GetProperties(PropertyType.ALL)
                    .Distinct()
                    .Order()
                    .Select(propertyId => new PropertyMetadata
                    {
                        Id = propertyId,
                        Units = Safe(() => simulationObject.GetPropertyUnit(propertyId)),
                        Description = Safe(() => simulationObject.GetPropertyDescription(propertyId)),
                    })
            ],
        };
    }

    private static object? ConvertJsonValue(JsonElement? value)
    {
        if (value is null)
        {
            return null;
        }

        var element = value.Value;
        return element.ValueKind switch
        {
            JsonValueKind.Number when element.TryGetInt32(out var intValue) => intValue,
            JsonValueKind.Number when element.TryGetDouble(out var doubleValue) => doubleValue,
            JsonValueKind.String => element.GetString(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => element.ToString(),
        };
    }

    private static string? Safe(Func<string> read)
    {
        try
        {
            return read();
        }
        catch
        {
            return null;
        }
    }
}
