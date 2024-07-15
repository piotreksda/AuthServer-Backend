using MediatR;
using OAuth2OpenId.Domain.Entities.EntitiesDtos;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Queries.GetClientList;

public class GetClientListQuery : IRequest<List<ClientDto>>
{
    public GetClientListQuery(int userId)
    {
        UserId = userId;
    }

    public int UserId { get; init; }
}