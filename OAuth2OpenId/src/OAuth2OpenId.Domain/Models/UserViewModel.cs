namespace OAuth2OpenId.Domain.Models;

public record UserViewModel
{
    public int Id { get; init; }
    public string UserName { get; init; }
}