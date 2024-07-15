using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Queries.GetClientDetail;

public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetailViewModel>
{
    private readonly IClientRepository _authClientRepository;

    public GetClientDetailQueryHandler(IClientRepository authClientRepository)
    {
        _authClientRepository = authClientRepository;
    }

    public async Task<ClientDetailViewModel> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // return await _authClientRepository.GetClientDetail(request.UserId, request.ClientId) ?? throw new Exception();
    }
}