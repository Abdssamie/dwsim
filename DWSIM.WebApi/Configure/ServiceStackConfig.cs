using DWSIM.WebApi.Data;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.PostgreSQL;

namespace DWSIM.WebApi.Configure;

public static class ServiceStackConfig
{
    public static void Configure(AppHostBase appHost, Funq.Container container, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");

        container.Register<ICacheClient>(new MemoryCacheClient());
        container.Register<IDbConnectionFactory>(
            new OrmLiteConnectionFactory(connectionString, PostgreSqlDialect.Provider));

        container.Register<IAuthRepository>(c =>
        {
            var authRepository = new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>());
            authRepository.InitSchema();
            return authRepository;
        });

        container.RegisterAutoWired<DwsimSessionStore>();

        appHost.Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[]
        {
            new JwtAuthProvider(appHost.AppSettings),
            new CredentialsAuthProvider(appHost.AppSettings),
        }));
    }
}
