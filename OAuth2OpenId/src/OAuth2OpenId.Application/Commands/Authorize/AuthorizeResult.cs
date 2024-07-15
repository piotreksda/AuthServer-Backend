namespace OAuth2OpenId.Application.Commands.Authorize;

public record AuthorizeResult
{
    public string? Code { get; set; }
    public string? State { get; set; }
    public string? IdToken { get; set; }
    public string? Token { get; set; }
    public int? expires_in { get; set; }
    public string? Error { get; set; }
    public string? ErrorDescription { get; set; }
}