using MediatR;

namespace OAuth2OpenId.Application.Commands.GrandTypes.CreateGrandType;

public class CreateGrandTypeCommand : IRequest
{
    public CreateGrandTypeCommand(int userId, Guid clientId, CreateGrandTypeInputModel model)
    {
        UserId = userId;
        Model = model;
        ClientId = clientId;
    }

    public int UserId { get; init; }
    public Guid ClientId { get; init; }
    public CreateGrandTypeInputModel Model { get; init; }
}