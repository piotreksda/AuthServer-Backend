namespace OAuth2OpenId.Application.Commands.Authorize;

public record AuthorizationRequest
{
    /// <summary>
    /// Specifies the type of authorization response expected from the server.
    /// </summary>
    /// <remarks>
    /// Values:
    /// code - for Authorization Code grant
    /// token - for implicit grant
    /// </remarks>
    public string response_type { get; init; }
    /// <summary>
    ///  The client identifier issued to the client during registration.
    /// </summary>
    public Guid client_id { get; init; }
    /// <summary>
    /// The URL to which the response will be sent. Must exactly match one of the pre-registered redirect URIs.
    /// </summary>
    public string redirect_uri { get; init; }
    /// <summary>
    /// A space-separated list of scopes that the application is requesting access to
    /// </summary>
    public string scope { get; init; }
    /// <summary>
    /// An opaque value used by the client to maintain state between the request and callback. The authorization server includes this value when redirecting the user-agent back to the client
    /// </summary>
    public string? state { get; init; }
    public string? code_challenge { get; init; }
    public string? code_challenge_method { get; init; }
    public string? nonce { get; init; }
    // public string? secret { get; init; }
}