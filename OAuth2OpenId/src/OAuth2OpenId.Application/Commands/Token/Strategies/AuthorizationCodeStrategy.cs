using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Token.Strategies;

public static class AuthorizationCodeStrategy
{
    public static Result<IStrategy> ChooseAuthenticator(TokenInputModel model, IServiceProvider serviceProvider)
    {
        return model.grant_type switch
        {
            "authorization_code" => Result.Ok<IStrategy>(new ValidateAuthorizationCode(model, serviceProvider.GetService<IUserRepository>()!)),
            _ => Result.Fail<IStrategy>("not_implemented")
        };
    }
}