using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Commands.RedirectUrls.CreateRedirectUrl;

public class CreateRedirectUrlCommandHandler : IRequestHandler<CreateRedirectUrlCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateRedirectUrlCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Handle(CreateRedirectUrlCommand request, CancellationToken cancellationToken)
    {
        Client client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken: cancellationToken) ?? throw new Exception();

        ClientEnvironment clientEnvironment =
            client.ClientEnvironments.FirstOrDefault(x => x.Id == request.Model.ClientEnvironmentId) ??
            throw new Exception();
        
        RedirectUri redirectUriToAdd = new RedirectUri(request.Model.ClientEnvironmentId, request.Model.Url);

        clientEnvironment.AddRedirectUri(redirectUriToAdd);

        await _clientRepository.SaveChangesAsync(cancellationToken);
    }
}