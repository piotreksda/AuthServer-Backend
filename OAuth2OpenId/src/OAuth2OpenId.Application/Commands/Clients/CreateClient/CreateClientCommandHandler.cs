using System.Security.Cryptography;
using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Commands.Clients.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{

    private readonly IClientRepository _authClientRepository;

    public CreateClientCommandHandler(IClientRepository authClientRepository)
    {
        _authClientRepository = authClientRepository;
    }

    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client clientToAdd = new Client(request.Model.Name, request.UserId, GenerateSecureSecret());
        
        await _authClientRepository.AddAsync(clientToAdd);

        await _authClientRepository.SaveChangesAsync();
    }

    //todo: create static helper
    private string GenerateSecureSecret()
    {
        var randomNumber = new byte[128];
        string refreshToken = "";
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}