namespace OAuth2OpenId.Application.Commands.Token;

public record TokenInputModel
{
    public string grant_type { get; init; }
    public string code { get; init; }
    public string redirect_uri { get; init; }
    public string? client_secret { get; init; }
    public string? code_verifier { get; init; }
    public string? nonce { get; init; }
    /*
     * grant_type: Set to "authorization_code".
       code: The authorization code received from the authorization server as a result of the user authorizing the application.
       redirect_uri: This must be the same redirect URI that was used in the initial authorization request (required if it was included in the authorization request).
       client_id: Optional depending on server requirements; needed if the client is not authenticating with the authorization server using client credentials.
       client_secret: Required for confidential clients.
       code_verifier: Used if the PKCE was initiated during the authorization request.
     */
}