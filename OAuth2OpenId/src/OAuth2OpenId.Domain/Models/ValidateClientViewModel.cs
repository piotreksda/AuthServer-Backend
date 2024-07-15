namespace OAuth2OpenId.Domain.Models;

public record ValidateClientViewModel
{
    public int RedirectUrlId { get; init; }
}