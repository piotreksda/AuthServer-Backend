namespace OAuth2OpenId.Domain.Models;

public record ClientDetailViewModel
{
    public Guid ClientId { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }

    public List<ClientEnvironmentListViewModel> ClientEnvironments { get; init; } =
        new List<ClientEnvironmentListViewModel>();

    public List<GrantTypeListViewModel> GrantTypes { get; init; } = new List<GrantTypeListViewModel>();

    public List<ScopesListViewModel> Scopes { get; init; } = new List<ScopesListViewModel>();
}

public record ClientEnvironmentListViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public record GrantTypeListViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public record ScopesListViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}