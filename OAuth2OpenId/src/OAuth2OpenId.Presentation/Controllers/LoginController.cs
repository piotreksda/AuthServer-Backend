using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Commands.Login;
using OAuth2OpenId.Application.Commands.Register;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel loginModel)
        {
            LoginCommand loginCommand = new LoginCommand(loginModel);

            ActionResult result = await _mediator.Send(loginCommand);

            return result;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputModel registerModel)
        {
            RegisterCommand loginCommand = new RegisterCommand(registerModel);

            var result = await _mediator.Send(loginCommand);

            return result;
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
        }
        
        [Authorize]
        [HttpGet("me")]
        public Task Me()
        {
            return Task.CompletedTask;
        }

        // [HttpPost("register")]
        // public async Task<IActionResult> PostRegister([FromBody] RegisterModel registerModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //
        //     var registerCommand = new RegisterCommand(new RegisterInputModel
        //     {
        //         UserName = registerModel.Username,
        //         Password = registerModel.Password
        //     });
        //
        //     Result result = await _mediator.Send(registerCommand);
        //
        //     if (result.IsFailure)
        //     {
        //         return BadRequest(new { Error = "Registration failed: " + result.Error });
        //     }
        //
        //     return Ok(new { Message = "Registration completed, now login" });
        // }

        // private async Task<IActionResult> SignInUser(UserViewModel userViewModel, string continueUrl)
        // {
        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, userViewModel.UserName),
        //         new Claim(ClaimTypes.NameIdentifier, userViewModel.Id.ToString())
        //         // Add other claims as necessary
        //     };
        //
        //     var claimsIdentity = new ClaimsIdentity(
        //         claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //
        //     var authProperties = new AuthenticationProperties
        //     {
        //         RedirectUri = continueUrl,
        //         IsPersistent = false
        //     };
        //
        //     await HttpContext.SignInAsync(
        //         CookieAuthenticationDefaults.AuthenticationScheme,
        //         new ClaimsPrincipal(claimsIdentity),
        //         authProperties);
        //
        //     return new RedirectResult(authProperties.RedirectUri);
        // }
    }
    //
    // public class Credentials
    // {
    //     [Required]
    //     public string Username { get; set; }
    //     [Required]
    //     public string Password { get; set; }
    //     public string Continue { get; set; }
    // }
    //
    // public class RegisterModel
    // {
    //     [Required]
    //     public string Username { get; set; }
    //     [Required]
    //     public string Password { get; set; }
    //     [Required, Compare("Password")]
    //     public string ConfirmPassword { get; set; }
    // }
    //
    // public class GetInputModel
    // {
    //     public string? Continue { get; init; }
    // }
}
