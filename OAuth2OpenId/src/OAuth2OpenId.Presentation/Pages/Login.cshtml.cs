// using System.ComponentModel.DataAnnotations;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;
// using OAuth2OpenId.Application.Commands.Login;
// using OAuth2OpenId.Application.Commands.Register;
// using OAuth2OpenId.Domain.Models;
//
// namespace OAuth2OpenId.Presentation.Pages;
// public class LoginModel : PageModel
// {
//     private readonly IMediator _mediator;
//
//     public LoginModel(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//
//     // [BindProperty]
//     // public Credentials Credentials { get; set; }
//     //
//     // [BindProperty]
//     // public RegisterModel RegisterDetails { get; set; }
//     
//     [BindProperty]
//     public string Continue { get; set; }
//
//     
//     public async Task<IActionResult> OnGetAsync([FromQuery] GetInputModel model)
//     {
//         Continue = model.Continue ?? "~/";
//         
//         if (HttpContext.User.Identity?.IsAuthenticated == true)
//             return Redirect(Continue);
//         
//         return Page();
//     }
//
//     public async Task<IActionResult> OnPostLoginAsync(Credentials credentials)
//     {
//         if (!ModelState.IsValid)
//         {
//             return Page();
//         }
//         
//         var loginCommand = new LoginCommand(new LoginInputModel
//         {
//             UserName = credentials.Username,
//             Password = credentials.Password
//         });
//         
//         Result<UserViewModel> result = await _mediator.Send(loginCommand);
//         
//         if (result.IsFailure)
//         {
//             ModelState.AddModelError("LoginFailed", "Invalid login attempt: " + result.Error);
//             ViewData["ShowForm"] = "LoginFailed";
//             return Page();
//         }
//         
//         return await SignInUser(result.Value!);
//         return Page();
//     }
//
//     public async Task<IActionResult> OnPostRegisterAsync(RegisterModel registerModel)
//     {
//         if (!ModelState.IsValid)
//         {
//             return Page();
//         }
//
//         var registerCommand = new RegisterCommand(new RegisterInputModel
//         {
//             UserName = registerModel.Username,
//             Password = registerModel.Password
//         });
//
//         Result result = await _mediator.Send(registerCommand);
//
//         if (result.IsFailure)
//         {
//             ModelState.AddModelError("RegisterFailed", "Registration failed: " + result.Error);
//             ViewData["ShowForm"] = "RegisterFailed";
//             return Page();
//         }
//         
//         ModelState.AddModelError("LoginFailed", "Registration completed now login");
//         ViewData["ShowForm"] = "LoginFailed";
//         return Page();
//     }
//
//     private async Task<IActionResult> SignInUser(UserViewModel userViewModel)
//     {
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.Name, userViewModel.UserName),
//             new Claim(ClaimTypes.NameIdentifier, userViewModel.Id.ToString())
//             // Add other claims as necessary
//         };
//
//         var claimsIdentity = new ClaimsIdentity(
//             claims, CookieAuthenticationDefaults.AuthenticationScheme);
//
//         var authProperties = new AuthenticationProperties
//         {
//             RedirectUri = Continue,
//             IsPersistent = true,
//             
//         };
//
//         await HttpContext.SignInAsync(
//             CookieAuthenticationDefaults.AuthenticationScheme,
//             new ClaimsPrincipal(claimsIdentity),
//             authProperties
//             );
//
//         return new RedirectResult(Continue);
//     }
// }
//
// public class Credentials
// {
//     [Required]
//     public string Username { get; set; }
//     [Required]
//     public string Password { get; set; }
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
