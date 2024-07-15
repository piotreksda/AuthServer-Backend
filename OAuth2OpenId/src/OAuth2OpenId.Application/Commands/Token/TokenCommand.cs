using MediatR;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Token;

public class TokenCommand : IRequest<Result<TokenResultModel>>
{
    public TokenCommand(TokenInputModel model)
    {
        Model = model;
    }

    public TokenInputModel Model { get; init; }
}