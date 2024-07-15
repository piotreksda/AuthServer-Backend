using System.Security.Cryptography;
using OAuth2OpenId.Domain.Entities.AgregateRoots;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Domain.Entities;

public class Client : AggregateRoot<Guid>
{
    private Client()
    {
        
    }

    public Client(string name, int ownerId, string secret)
    {
        Name = name;
        OwnerId = ownerId;
        Secret = secret;
        IsActive = true;
    }
    
    //todo: add valueobject
    public string Secret { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    
    // public User Owner { get; private set; }
    public int OwnerId { get; private set; }

    public List<ClientEnvironment> ClientEnvironments { get; private set; } = new List<ClientEnvironment>();
    public List<ClientGrand> AllowedGrantTypes { get; private set; } = new List<ClientGrand>();
    public List<ClientScope> AllowedScopes { get; private set; } = new List<ClientScope>();

    public void AddClientEnvironment(ClientEnvironment clientEnvironment)
    {
        ClientEnvironments.Add(clientEnvironment);
    }

    public void AddGrantType(ClientGrand clientGrand)
    {
        AllowedGrantTypes.Add(clientGrand);
    }

    public void AddAllowedScopes(ClientScope clientScope)
    {
        AllowedScopes.Add(clientScope);
    }
    // public List<AuthorizationCode> AuthorizationCodes { get; private set; } = new List<AuthorizationCode>();
    // public List<AccessToken> AccessTokens { get; private set; } = new List<AccessToken>();
    // public List<IdToken> IdTokens { get; private set; } = new List<IdToken>();
    // public List<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
}