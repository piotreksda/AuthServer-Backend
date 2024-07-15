using MediatR;

namespace OAuth2OpenId.Application.Queries.UserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Dictionary<string, object>>
{
    public Task<Dictionary<string, object>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        //1. user and scopes from token
        //2. based on scopes prepare dict
        throw new NotImplementedException();
    }
}