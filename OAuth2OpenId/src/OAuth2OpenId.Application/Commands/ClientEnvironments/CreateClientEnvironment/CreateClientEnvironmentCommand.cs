using MediatR;

namespace OAuth2OpenId.Application.Commands.ClientEnvironments.CreateClientEnvironment;

public class CreateClientEnvironmentCommand : IRequest
{
    public CreateClientEnvironmentCommand(int userId, Guid clientId, CreateClientEnvironmentInputModel model)
    {
        UserId = userId;
        Model = model;
        ClientId = clientId;
    }

    public int UserId { get; init; }
    public Guid ClientId { get; init; }
    public CreateClientEnvironmentInputModel Model { get; init; }
}