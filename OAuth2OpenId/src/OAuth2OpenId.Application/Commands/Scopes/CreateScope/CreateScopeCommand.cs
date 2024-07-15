using MediatR;

namespace OAuth2OpenId.Application.Commands.Scopes.CreateScope;

public class CreateScopeCommand : IRequest
{
    public CreateScopeCommand(int userId, Guid clientId, CreateScopeInputModel model)
    {
        UserId = userId;
        Model = model;
        ClientId = clientId;
    }

    public int UserId { get; init; }
    public Guid ClientId { get; init; }
    public CreateScopeInputModel Model { get; init; }
}