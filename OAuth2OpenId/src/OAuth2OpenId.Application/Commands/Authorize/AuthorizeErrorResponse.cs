namespace OAuth2OpenId.Application.Commands.Authorize;

public class AuthorizeErrorResponse
{
    public string error { get; init; }
    public string error_description { get; init; }
    public string error_uri { get; init; }
}