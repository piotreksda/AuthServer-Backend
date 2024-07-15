namespace OAuth2OpenId.Application.Commands.Introspect;

public record IntrospectResultDto
{
    public string Scope { get; init; }
    public bool Active { get; init; }
    public Guid Client_id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string Sub { get; init; }
    public string Aud { get; init; }
    public string Iss { get; init; }
    public int Exp { get; init; }
    public int Iat { get; init; }
}