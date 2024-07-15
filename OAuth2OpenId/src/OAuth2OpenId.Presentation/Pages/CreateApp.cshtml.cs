using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth2OpenId.Application.Commands.Clients.CreateClient;
using OAuth2OpenIdPresentation.Extensions;

namespace OAuth2OpenId.Presentation.Pages;

public class CreateApp : PageModel
{
    private readonly IMediator _mediator;

    public CreateApp(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostCreateAppAsync(CreateClientInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userId = HttpContext.GetUserId();

        if (userId.IsFailure)
        {
            throw new Exception();
        }
        
        var CreateAppCommand = new CreateClientCommand(userId.Value!, model);

        await _mediator.Send(CreateAppCommand);

        return Page();

    }
}