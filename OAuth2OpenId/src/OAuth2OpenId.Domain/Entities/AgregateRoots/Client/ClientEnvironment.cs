namespace OAuth2OpenId.Domain.Entities;

public class ClientEnvironment : BaseEntity<int>
{
    private ClientEnvironment()
    {
        
    }

    public ClientEnvironment(string name, Guid clientId)
    {
        Name = name;
        ClientId = clientId;
    }
    
    public string Name { get; init; }
    
    // public Client Client { get; init; }
    public Guid ClientId { get; init; }
    public List<RedirectUri> RedirectUris { get; private set; }
    
    public List<ResourceServersClientEnvironments> ResourceServersClientEnvironments { get; init; } =
        new List<ResourceServersClientEnvironments>();

    public void AddRedirectUri(RedirectUri redirectUri)
    {
        RedirectUris.Add(redirectUri);
    }
}