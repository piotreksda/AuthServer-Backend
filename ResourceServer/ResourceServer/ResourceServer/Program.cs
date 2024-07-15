using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ResourceServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();

builder.Services.AddAuthenticationExtension();

builder.Services.AddCorsExtension();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add endpoints
app.MapGet("/", () => "Hello World!");

app.MapGet("/login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    await httpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = returnUrl, IsPersistent = true});
});

app.MapGet("/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    await httpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
});

app.MapGet("/secure", [Authorize] async (HttpContext httpContext, ApplicationDbContext dbContext) =>
{
    var test = httpContext.Request.Cookies.FirstOrDefault(x => x.Key == "AuthCookie_RS");
    var userId = int.Parse(httpContext.User.Claims
        .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
    return $"welcome user {userId}, in-memory database has user: {user is not null}";
});

app.Run();