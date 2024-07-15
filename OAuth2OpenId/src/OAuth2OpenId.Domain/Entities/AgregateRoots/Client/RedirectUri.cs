namespace OAuth2OpenId.Domain.Entities;

public class RedirectUri : BaseEntity<int>
{
    private RedirectUri()
    {
        
    }

    public RedirectUri(int clientEnvironmentId, string uri)
    {
        ClientEnvironmentId = clientEnvironmentId;
        Uri = uri;
    }
    
    public string Uri { get; private set; }
    
    // public ClientEnvironment ClientEnvironment { get; private set; }
    public int ClientEnvironmentId { get; private set; }
    // public List<AuthorizationCode> AuthorizationCodes { get; private set; }
}