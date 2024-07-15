using Microsoft.Extensions.Options;

namespace OAuth2OpenId.Application.Services;

public static class ExtensionMethods
{
    public static IServiceCollection AddUserService(this IServiceCollection services, Action<UserServiceOptions> configureOptions)
    {
        services.Configure(configureOptions);

        services.AddScoped<UserService>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<UserServiceOptions>>().Value;
            return new UserService(options);
        });

        return services;
    }
}