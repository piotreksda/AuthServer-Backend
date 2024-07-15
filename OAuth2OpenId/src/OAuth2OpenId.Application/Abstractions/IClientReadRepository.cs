using OAuth2OpenId.Domain.Entities.EntitiesDtos;

namespace OAuth2OpenId.Application.Abstractions;

public interface IClientReadRepository
{
    Task<List<ClientDto>> GetClients(CancellationToken cancellationToken = default);
}