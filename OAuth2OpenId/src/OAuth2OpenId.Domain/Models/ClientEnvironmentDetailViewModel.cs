namespace OAuth2OpenId.Domain.Models;

public record ClientEnvironmentDetailViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<RedirectUriListViewModel> RedirectUris { get; init; }
}

public record RedirectUriListViewModel
{
    public int Id { get; init; }
    public string Uri { get; init; }
}