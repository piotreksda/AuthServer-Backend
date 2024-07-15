using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Commands.ClientEnvironments.CreateClientEnvironment;

public class CreateClientEnvironmentCommandHandler : IRequestHandler<CreateClientEnvironmentCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientEnvironmentCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Handle(CreateClientEnvironmentCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken: cancellationToken) ?? throw new Exception();
        
        ClientEnvironment clientEnvironmentToAdd = new ClientEnvironment(request.Model.Name, request.Model.ClientId);

        client.AddClientEnvironment(clientEnvironmentToAdd);

        await _clientRepository.SaveChangesAsync(cancellationToken);
    }
}