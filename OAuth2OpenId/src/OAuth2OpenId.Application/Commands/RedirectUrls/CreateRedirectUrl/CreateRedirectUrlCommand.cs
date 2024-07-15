using MediatR;

namespace OAuth2OpenId.Application.Commands.RedirectUrls.CreateRedirectUrl;

public class CreateRedirectUrlCommand : IRequest
{
    public CreateRedirectUrlCommand( Guid clientId, CreateRedirectUrlInputModel model)
    {
        Model = model;
        ClientId = clientId;
    }

    public CreateRedirectUrlInputModel Model { get; init; }
    public Guid ClientId { get; init; }
}