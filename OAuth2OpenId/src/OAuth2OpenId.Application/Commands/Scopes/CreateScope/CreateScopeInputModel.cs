namespace OAuth2OpenId.Application.Commands.Scopes.CreateScope;

public record CreateScopeInputModel
{
    public string Name { get; init; }
    public Guid ClientId { get; init; }
}