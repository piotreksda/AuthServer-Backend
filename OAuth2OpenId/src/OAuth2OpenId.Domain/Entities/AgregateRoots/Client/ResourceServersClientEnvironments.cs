namespace OAuth2OpenId.Domain.Entities;

public class ResourceServersClientEnvironments
{
    public int ClientEnvironmentId { get; init; }
    public ClientEnvironment ClientEnvironment { get; init; }
    
    public int ResourceServerId { get; init; }
    public ResourceServer ResourceServer { get; init; }
}