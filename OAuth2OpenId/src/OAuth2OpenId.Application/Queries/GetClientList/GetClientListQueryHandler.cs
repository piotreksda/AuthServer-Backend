using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities.EntitiesDtos;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Queries.GetClientList;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, List<ClientDto>>
{
    private readonly IClientReadRepository _clientReadRepository;

    public GetClientListQueryHandler(IClientReadRepository clientReadRepository)
    {
        _clientReadRepository = clientReadRepository;
    }

    public async Task<List<ClientDto>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {
        return await _clientReadRepository.GetClients(cancellationToken);
    }
}