using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Commands.Scopes.CreateScope;

public class CreateScopeCommandHandler : IRequestHandler<CreateScopeCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateScopeCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Handle(CreateScopeCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken: cancellationToken) ??
                        throw new Exception();
        
        ClientScope clientScopeToAdd = new ClientScope(request.Model.Name, request.Model.ClientId);

        client.AllowedScopes.Add(clientScopeToAdd);

        await _clientRepository.SaveChangesAsync(cancellationToken);
    }
}