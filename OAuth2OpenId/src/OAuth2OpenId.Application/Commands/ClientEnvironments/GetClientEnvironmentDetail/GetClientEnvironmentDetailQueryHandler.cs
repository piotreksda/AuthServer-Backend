using MediatR;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.ClientEnvironments.GetClientEnvironmentDetail;

// public class GetClientEnvironmentDetailQueryHandler : IRequestHandler<GetClientEnvironmentDetailQuery, ClientEnvironmentDetailViewModel >
// {
    // private readonly IClientEnvironmentRepository _clientEnvironmentRepository;

    // public GetClientEnvironmentDetailQueryHandler(IClientEnvironmentRepository clientEnvironmentRepository)
    // {
        // _clientEnvironmentRepository = clientEnvironmentRepository;
    // }

    // public async Task<ClientEnvironmentDetailViewModel> Handle(GetClientEnvironmentDetailQuery request, CancellationToken cancellationToken)
    // {
        // throw new NotImplementedException();
        // return await _clientEnvironmentRepository.ClientEnvironmentDetail(request.ClientEnvironmentId) ??
        //        throw new Exception();
    // }
// }