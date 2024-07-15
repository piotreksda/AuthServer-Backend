using MediatR;

namespace OAuth2OpenId.Application.Commands.Introspect;

public class IntrospectCommandHandler : IRequestHandler<IntrospectCommand, IntrospectResultDto>
{
    public async Task<IntrospectResultDto> Handle(IntrospectCommand request, CancellationToken cancellationToken)
    {
        return new IntrospectResultDto
        {
            Active = false,
            Client_id = Guid.NewGuid(),
            Username = "jdoe",
            Scope = "read write dolphin",
            Sub = "Z5O3upPC88QrAjx00dis",
            Aud = "https=//protected.example.net/resource",
            Iss = "https=//server.example.com/",
            Exp = 1419356238,
            Iat = 1419350238,
        };
    }
}