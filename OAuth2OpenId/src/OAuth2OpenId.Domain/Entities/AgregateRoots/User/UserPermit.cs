namespace OAuth2OpenId.Domain.Entities;

public class UserPermit
{
    public UserPermit()
    {
        
    }
    public UserPermit(int userId, ClientScope scope)
    {
        UserId = userId;
        ClientScope = scope;
    }
    public int UserId { get; private set; }
    // public User User { get; private set; }
    
    public int ClientScopeId { get; private set; }
    public ClientScope ClientScope { get; private set; }
}