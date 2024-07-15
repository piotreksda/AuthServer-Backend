using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Configure;

public static class ConfigureWebApplication
{
    public static AuthDbContextInitializer InitializeDatabase(this IServiceProvider services)
    {
        IServiceScope scope = services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<AuthDbContextInitializer>();
    }
}