using System.Security.Cryptography;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Commands.Authorize;
using OAuth2OpenId.Application.Commands.Introspect;
using OAuth2OpenId.Application.Commands.Token;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenIdPresentation.Controllers;

[ApiController]
public class OAuth2Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public OAuth2Controller(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("/authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizationRequest request)
    {
        AuthorizeCommand command = new AuthorizeCommand(request);

        return await _mediator.Send(command);
    }
    
    [HttpPost("/introspect")]
    public async Task<IntrospectResultDto> Introspect([FromForm] string token, [FromForm] string? token_type_hint, [FromForm] Guid client_id, [FromForm] string? client_secret)
    {
        IntrospectCommand command = new IntrospectCommand(token, token_type_hint, client_id, client_secret);
        
        var result = await _mediator.Send(command);

        return result;
    }
    
    [HttpPost("/device/code")]
    public async Task DeviceCode()
    {
        
    }

    [HttpPost("/token")]
    public async Task<ActionResult<TokenResultModel>> Token([FromForm] TokenInputModel model)
    {
        TokenCommand command = new TokenCommand(model);

        Result<TokenResultModel> result = await _mediator.Send(command);
        if (result.IsFailure)
            return new BadRequestObjectResult(result.Error);
        return result.Value!;
    }
    
    [HttpPost("/revoke")]
    public async Task Revoke()
    {
        // Here you would revoke a previously issued token (access or refresh token).
    }

}