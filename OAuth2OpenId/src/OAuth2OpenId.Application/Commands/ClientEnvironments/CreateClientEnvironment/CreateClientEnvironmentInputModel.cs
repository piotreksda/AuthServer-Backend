namespace OAuth2OpenId.Application.Commands.ClientEnvironments.CreateClientEnvironment;

public record CreateClientEnvironmentInputModel
{
    public string Name { get; init; }
    public Guid ClientId { get; init; }
    
}