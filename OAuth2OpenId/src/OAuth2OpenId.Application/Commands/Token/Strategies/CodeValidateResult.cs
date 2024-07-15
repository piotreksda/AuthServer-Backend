namespace OAuth2OpenId.Application.Commands.Token.Strategies;

public record CodeValidateResult
{
    public Guid ClientId { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
}