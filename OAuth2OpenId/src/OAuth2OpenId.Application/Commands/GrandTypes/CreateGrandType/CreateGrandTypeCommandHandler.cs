using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Commands.GrandTypes.CreateGrandType;

public class CreateGrandTypeCommandHandler : IRequestHandler<CreateGrandTypeCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateGrandTypeCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task Handle(CreateGrandTypeCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken: cancellationToken) ??
                        throw new Exception();
        ClientGrand clientGrandToAdd = new ClientGrand(request.Model.Name, request.Model.ClientId);

        client.AddGrantType(clientGrandToAdd);

        await _clientRepository.SaveChangesAsync(cancellationToken);
    }
}