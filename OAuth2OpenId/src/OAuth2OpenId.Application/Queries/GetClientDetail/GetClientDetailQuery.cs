using MediatR;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Queries.GetClientDetail;

public class GetClientDetailQuery : IRequest<ClientDetailViewModel>
{
    public GetClientDetailQuery(int userId, Guid clientId)
    {
        UserId = userId;
        ClientId = clientId;
    }

    public int UserId { get; init; }
    public Guid ClientId { get; init; }
}