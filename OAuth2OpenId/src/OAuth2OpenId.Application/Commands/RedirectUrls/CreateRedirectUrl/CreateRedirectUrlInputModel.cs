namespace OAuth2OpenId.Application.Commands.RedirectUrls.CreateRedirectUrl;

public record CreateRedirectUrlInputModel
{
    public int ClientEnvironmentId { get; init; }
    public string Url { get; init; }
}