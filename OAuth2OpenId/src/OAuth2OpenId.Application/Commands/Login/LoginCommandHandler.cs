using System.Security.Claims;
using System.Text;
using Konscious.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Models;
using OAuth2OpenId.Domain.ValueObjects;

namespace OAuth2OpenId.Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ActionResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Model.UserName);
        if (user == null)
            return new NotFoundObjectResult("User not found");

        if (!user.Password.CheckPassword(request.Model.Password))
        {
            return new BadRequestObjectResult("Invalid password");
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            RedirectUri = request.Model.Continue,
            IsPersistent = true,
            
        };

        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        if (request.Model.Continue is not null)
            return new RedirectResult(request.Model.Continue);

        return new OkResult();
    }
}