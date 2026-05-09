using DWSIM.WebApi;
using DWSIM.WebApi.Services;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPlugin(new CommandsFeature());
builder.Services.AddServiceStack(typeof(SessionServices).Assembly);

var app = builder.Build();

app.UseServiceStack(new AppHost(builder.Configuration));

app.Run();
