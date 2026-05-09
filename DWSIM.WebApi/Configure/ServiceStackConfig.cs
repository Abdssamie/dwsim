using DWSIM.WebApi.Data;
using DWSIM.Automation;
using ServiceStack;
using ServiceStack.Caching;

namespace DWSIM.WebApi.Configure;

public static class ServiceStackConfig
{
    public static void Configure(AppHostBase appHost, Funq.Container container, IConfiguration configuration)
    {
        container.Register<ICacheClient>(new MemoryCacheClient());
        container.Register<AutomationInterface>(new DWSIM.Automation.Automation());
        container.RegisterAutoWiredAs<SessionStore, SessionStore>();
    }
}
