namespace OAuth2OpenId.Domain.Entities;

public class AuthorizationCode : BaseEntity<int>
{
    public AuthorizationCode()
    {
        
    }

    public AuthorizationCode(Guid clientId, int userId, int redirectUriId, string code, TimeSpan lifeTime, string scope,
        string? challengeVerifier = null, string? challengeVerifierMethod = null, string? nonce = null)
    {
        ClientId = clientId;
        UserId = userId;
        RedirectUriId = redirectUriId;
        Code = code;
        ExpiryDate = DateTime.UtcNow.Add(lifeTime);
        Scope = scope;
        ChallengeVerifier = challengeVerifier;
        ChallengeVerifierMethod = challengeVerifierMethod;
        Nonce = nonce;
    }
    public string Code { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public string Scope { get; private set; }
    public bool IsUsed { get; private set; }
    public string? Nonce { get; private set; }
    
    
    // public Client Client { get; private set; }
    public Guid ClientId { get; private set; }
    // public User User { get; private set; }
    public int UserId { get; private set; }
    // public RedirectUri RedirectUri { get; private set; }
    public int RedirectUriId { get; private set; }
    public string? ChallengeVerifier { get; private set; }
    public string? ChallengeVerifierMethod { get; private set; }

    public void UseToken()
    {
        IsUsed = true;
    }
}