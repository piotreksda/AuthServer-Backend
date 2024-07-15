using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ResourceServer;

public static class ConfigureServicesExtensions
{
    public static void AddDatabase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: "database_name");
        });
    }
    public static void AddAuthenticationExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISingUpService, SingUpService>();

        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "AuthCookie_RS";
                options.LoginPath = "/login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.SlidingExpiration = true;

                // options.Events.OnRedirectToLogin += async context =>
                // {
                //
                // };
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:7234";
                options.ClientId = "84ccd593-947d-4ff2-b37d-5ffb08306e86";
                options.ClientSecret =
                    "123";
                options.ResponseType = "code";
                options.UsePkce = true;
                options.SaveTokens = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");

                options.GetClaimsFromUserInfoEndpoint = true;

                options.Events.OnTicketReceived += SingUpService.CreateOnSignUp;
            });
        // .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        // {
        //     options.Authority = "https://localhost:7234";
        //     options.Audience = "20c42dbf-b59f-498f-9287-30e2ec0916f3"; // replace with your resource server's audience
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,
        //         ValidIssuer = "https://localhost:7234",
        //         ValidateAudience = true,
        //         ValidAudience = "20c42dbf-b59f-498f-9287-30e2ec0916f3",
        //         ValidateLifetime = true,
        //         ClockSkew = TimeSpan.Zero // optional: set to zero to eliminate allowed clock skew
        //     };
        //
        //     options.Events = new JwtBearerEvents
        //     {
        //         OnAuthenticationFailed = async context =>
        //         {
        //             if (context.Exception is SecurityTokenException or SecurityTokenMalformedException)
        //             {
        //                 context.Response.StatusCode = 401;
        //                 
        //                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        //                     context.Response.Headers.Append("Token-Expired", "true");
        //
        //                 // if (context.Exception is SecurityTokenInvalidSignatureException)
        //                 //     context.Response.Headers.Append("Token-Invalid-Signature", "true");
        //                 
        //                 await context.Response.CompleteAsync();
        //             }
        //         }
        //     };
        // });

        serviceCollection.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme)
                .Build();
        });
    }

    public static void AddCorsExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}