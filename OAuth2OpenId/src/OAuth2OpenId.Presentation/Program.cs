using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OAuth2OpenId.Application;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Services;
using OAuth2OpenId.Application.Services.Interfaces;
using OAuth2OpenId.Infrastructure.Configure;
using OAuth2OpenId.Infrastructure.Repositories;
using OAuth2OpenId.Infrastructure.Services;

namespace OAuth2OpenId.Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IKeyManager keyManager = new KeyManager();
        builder.Services.AddSingleton<IKeyManager>(keyManager);
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins("https://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Login";
                options.Cookie.Name = "AuthCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = async context =>
                {
                    context.Response.StatusCode = 401;
                    await context.Response.CompleteAsync();
                };
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };

                options.Configuration = new()
                {
                    SigningKeys = { new RsaSecurityKey(keyManager.OpenIdKey) }
                };
            });
        
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                // .RequireClaim("test", "true")
                .Build();
            
            options.AddPolicy("oauth",  new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .RequireClaim(JwtRegisteredClaimNames.Jti)
                .Build());
        });
        
        builder.Services.AddDatabaseConfiguration(builder.Configuration);

        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IClientRepository, ClientRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IClientReadRepository, ClientReadRepository>();
        builder.Services.AddTransient<ITokenService, TokenService>();

        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(dump).Assembly);
        });

        var app = builder.Build();

        await app.Services.InitializeDatabase().InitializeAsync();

        app.UseCors();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        
        app.UseStaticFiles();

        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseAuthentication();
        
        app.UseAuthorization();

        app.MapRazorPages();

        await app.RunAsync();
    }
}