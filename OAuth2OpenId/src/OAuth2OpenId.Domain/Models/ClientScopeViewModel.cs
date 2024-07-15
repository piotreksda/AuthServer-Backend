namespace OAuth2OpenId.Domain.Models;

public record ClientScopeViewModel
{
    public int ClientScopeId { get; init; }
    public string ScopeName { get; init; }
    public bool Accepted { get; init; }
}