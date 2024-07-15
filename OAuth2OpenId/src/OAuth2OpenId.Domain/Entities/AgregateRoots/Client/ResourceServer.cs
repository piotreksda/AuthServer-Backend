namespace OAuth2OpenId.Domain.Entities;

public class ResourceServer
{
    public int Id { get; init; }
    
    public string Name { get; init; }
    public string Url { get; init; }

    public List<ResourceServersClientEnvironments> ResourceServersClientEnvironments { get; init; } =
        new List<ResourceServersClientEnvironments>();
}