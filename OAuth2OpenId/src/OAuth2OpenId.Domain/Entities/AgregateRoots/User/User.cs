using OAuth2OpenId.Domain.Entities.AgregateRoots;
using OAuth2OpenId.Domain.ValueObjects;

namespace OAuth2OpenId.Domain.Entities;

public class User : AggregateRoot<int>
{
    private User()
    {
        AccessTokens = new List<AccessToken>();
        AuthorizationCodes = new List<AuthorizationCode>();
        RefreshTokens = new List<RefreshToken>();
        UserPermits = new List<UserPermit>();
    }
    public User(string username, string email, Password password, bool isMfaEnabled)
    {
        Username = username;
        NormalizedUsername = username.ToUpper();
        Email = email;
        NormalizedEmail = email.ToUpper();
        Password = password;
        IsMfaRequired = isMfaEnabled;
        AccessTokens = new List<AccessToken>();
        AuthorizationCodes = new List<AuthorizationCode>();
        RefreshTokens = new List<RefreshToken>();
        UserPermits = new List<UserPermit>();
    }
    public string Username { get; private set; }
    public string NormalizedUsername { get; private set; }
    public Password Password { get; private set; }
    public bool IsAnonymous { get; private set; } = false;
    
    public string Email { get; private set; }
    public string NormalizedEmail { get; private set; }
    
    public int BadAttemptCount { get; private set; }
    
    public bool IsLockedDown { get; private set; }
    public DateTime? LockedDownUntil { get; private set; }

    public bool IsMfaRequired { get; private set; } = false;
    
    
    public List<AccessToken> AccessTokens { get; private set; } = new List<AccessToken>();
    public List<IdToken> IdTokens { get; private set; } = new List<IdToken>();
    public List<AuthorizationCode> AuthorizationCodes { get; private set; } = new List<AuthorizationCode>();
    public List<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
    public List<UserPermit> UserPermits { get; private set; } = new List<UserPermit>();

    public void DisableMfa()
    {
        IsMfaRequired = false;
        // IsLockedDown = 132
    }
}