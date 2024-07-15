using MediatR;

namespace OAuth2OpenId.Application.Commands.Introspect;

public class IntrospectCommand : IRequest<IntrospectResultDto>
{
    public string token;
    public string? token_type_hint;
    public Guid client_id;
    public string? client_secret;

    public IntrospectCommand(string token, string? tokenTypeHint, Guid clientId, string? clientSecret)
    {
        this.token = token;
        token_type_hint = tokenTypeHint;
        client_id = clientId;
        client_secret = clientSecret;
    }
}