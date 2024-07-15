namespace OAuth2OpenId.Application.Commands.GrandTypes.CreateGrandType;

public record CreateGrandTypeInputModel
{
    public string Name { get; init; }
    public Guid ClientId { get; init; }
}