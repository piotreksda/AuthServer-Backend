namespace OAuth2OpenId.Domain.Entities;

public class RefreshToken
{
    public RefreshToken()
    {
        
    }

    public RefreshToken(Guid clientId, int userId, string token, TimeSpan liveTime)
    {
        ClientId = clientId;
        UserId = userId;
        Token = token;
        ExpiryDate = DateTime.UtcNow.Add(liveTime);
    }
    public int Id { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsUsed { get; private set; }

    // public Client Client { get; private set; }
    public Guid ClientId { get; private set; }
    // public User User { get; private set; }
    public int UserId { get; private set; }
}