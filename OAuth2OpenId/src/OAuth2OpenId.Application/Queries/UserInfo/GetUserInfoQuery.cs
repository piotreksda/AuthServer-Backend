using MediatR;

namespace OAuth2OpenId.Application.Queries.UserInfo;

public class GetUserInfoQuery : IRequest<Dictionary<string, object>>
{
    public GetUserInfoQuery(Guid jwi)
    {
        Jwi = jwi;
    }

    public Guid Jwi { get; init; }
}