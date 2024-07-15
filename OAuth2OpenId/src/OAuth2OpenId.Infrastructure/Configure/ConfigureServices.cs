using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OAuth2OpenId.Domain.Configurations;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Configure;

public static class ConfigureServices
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        DatabaseOptions dbOptions = new();
        configuration.GetSection(DatabaseOptions.Section).Bind(dbOptions);

        dbOptions.Validate();

        services.AddSingleton(dbOptions);

        DbConnectionStringBuilder connectionStringBuilder = new()
        {
            { "Server", dbOptions.Host },
            { "Database", dbOptions.Name },
            { "Port", dbOptions.Port },
            { "Username", dbOptions.User },
            { "Password", dbOptions.Password },
            { "Keepalive", dbOptions.Keepalive }
        };

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(connectionStringBuilder.ConnectionString, b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName));
        });

        
        services.AddDbContext<AuthReadOnlyDbContext>(options =>
        {
            options.UseNpgsql(connectionStringBuilder.ConnectionString);
        });
        
        services.AddScoped<AuthDbContextInitializer>();

        return services;
    }
    
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(
            c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OAuth2.0 OpenId server"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
            });

        return services;
    }
}