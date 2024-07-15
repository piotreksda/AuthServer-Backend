namespace OAuth2OpenId.Application.Commands.Token;

public record TokenResultModel
{
    public string? id_token { get; init; }
    public string? access_token { get; init; }
    public string token_type { get; init; }
    public int expires_in { get; init; }
    public string? refresh_token { get; init; }
}