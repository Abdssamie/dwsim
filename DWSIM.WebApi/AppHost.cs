using DWSIM.WebApi.Configure;
using DWSIM.WebApi.Services;
using ServiceStack;

namespace DWSIM.WebApi;

public sealed class AppHost(IConfiguration configuration)
    : AppHostBase("Cloud API", typeof(SessionServices).Assembly)
{
    public override void Configure(Funq.Container container)
    {
        ServiceStackConfig.Configure(this, container, configuration);
    }
}
