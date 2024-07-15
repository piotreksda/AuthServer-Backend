using MediatR;

namespace OAuth2OpenId.Application.Commands.Clients.CreateClient;

public class CreateClientCommand : IRequest
{
    public CreateClientCommand(int userId, CreateClientInputModel model)
    {
        UserId = userId;
        Model = model;
    }
    
    public int UserId { get; init; }
    public CreateClientInputModel Model { get; init; }
}