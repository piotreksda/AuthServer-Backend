namespace OAuth2OpenId.Domain.Models;

public record ClientListViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}