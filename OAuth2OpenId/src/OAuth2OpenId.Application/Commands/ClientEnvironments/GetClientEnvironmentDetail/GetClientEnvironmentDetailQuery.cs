using MediatR;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.ClientEnvironments.GetClientEnvironmentDetail;

public class GetClientEnvironmentDetailQuery : IRequest<ClientEnvironmentDetailViewModel>
{
    public GetClientEnvironmentDetailQuery(int userId, int clientEnvironmentId)
    {
        UserId = userId;
        ClientEnvironmentId = clientEnvironmentId;
    }

    public int UserId { get; init; }
    public int ClientEnvironmentId { get; init; }
}