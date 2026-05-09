using DWSIM.WebApi;
using DWSIM.WebApi.Infrastructure.Jobs;
using DWSIM.WebApi.Services;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPlugin(new CommandsFeature());
builder.Services.AddPlugin(new DatabaseJobFeature());
builder.Services.AddHostedService<JobsHostedService>();
builder.Services.AddServiceStack(typeof(SessionServices).Assembly);

var app = builder.Build();

app.UseServiceStack(new AppHost(builder.Configuration));

app.Run();
