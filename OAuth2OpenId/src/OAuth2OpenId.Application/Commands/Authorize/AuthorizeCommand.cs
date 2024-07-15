using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OAuth2OpenId.Application.Commands.Authorize;

public class AuthorizeCommand : IRequest<ActionResult>
{
    public AuthorizeCommand(AuthorizationRequest request)
    {
        Request = request;
    }

    public AuthorizationRequest Request { get; init; }
}