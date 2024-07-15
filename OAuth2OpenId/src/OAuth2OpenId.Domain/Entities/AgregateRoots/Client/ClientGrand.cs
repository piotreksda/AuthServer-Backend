namespace OAuth2OpenId.Domain.Entities;

public class ClientGrand : BaseEntity<int>
{
    private ClientGrand()
    {
        
    }

    public ClientGrand(string name, Guid clientId)
    {
        Name = name;
        ClientId = clientId;
    }
    public string Name { get; private set; }
    
    // public Client Client { get; private set; }
    public Guid ClientId { get; private set; }
}