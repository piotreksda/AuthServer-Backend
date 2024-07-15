namespace OAuth2OpenId.Domain.Entities;

public class AccessToken
{
    private AccessToken()
    {
        
    }

    public AccessToken(Guid jwi, Guid clientId, int userId, TimeSpan lifeTime)
    {
        Jwi = jwi;
        ClientId = clientId;
        UserId = userId;
        ExpiryDate = DateTime.UtcNow.Add(lifeTime);
        IsBlocked = false;
    }
    public int Id { get; private set; }
    public Guid Jwi { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsBlocked { get; private set; }
    // public Client Client { get; private set; }
    public Guid ClientId { get; private set; }
    // public User User { get; private set; }
    public int UserId { get; private set; }

}