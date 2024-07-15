namespace OAuth2OpenId.Domain.Entities;

public class ClientScope : BaseEntity<int>
{
    private ClientScope()
    {
        
    }

    public ClientScope(string name, Guid clientId)
    {
        Name = name;
        ClientId = clientId;
    }
    public string Name { get; private set; }
    public Guid ClientId { get; private set; }
}