using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Commands.Token.Strategies;
using OAuth2OpenId.Application.Services.Interfaces;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace OAuth2OpenId.Application.Commands.Token;

public class TokenCommandHandler : IRequestHandler<TokenCommand, Result<TokenResultModel>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IClientRepository _authClientRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITokenService _tokenService;
    
    public TokenCommandHandler(IHttpContextAccessor httpContextAccessor, IClientRepository authClientRepository, IServiceProvider serviceProvider, ITokenService tokenService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authClientRepository = authClientRepository;
        _serviceProvider = serviceProvider;
        _tokenService = tokenService;
    }

    public async Task<Result<TokenResultModel>> Handle(TokenCommand request, CancellationToken cancellationToken)
    {
        Result validateRequest = ValidateRequest(request.Model);
        if (validateRequest.IsFailure)
            return Result.Fail<TokenResultModel>(validateRequest.Error);
        
        Result<AuthenticateForTokenResult> authorizeRequestResult = await AuthorizeRequest(request.Model, _serviceProvider);
        if (authorizeRequestResult.IsFailure)
            return Result.Fail<TokenResultModel>(authorizeRequestResult.Error);
        
        return await GenerateResponse(request.Model, authorizeRequestResult.Value!.User, authorizeRequestResult.Value!.ClientId, authorizeRequestResult.Value!.Scopes, authorizeRequestResult.Value!.Nonce);
    }

    private async Task<Result<TokenResultModel>> GenerateResponse(TokenInputModel model, User user, Guid clientId, string scopes, string? nonce)
    {
        string? refreshTokenValue = null;
        string? idTokenValue = null;
        
        var accessTokenLifeTime = new TimeSpan(0, 0, 2, 0, 0);
        var accessToken = _tokenService.GenerateAccessToken(user, clientId, accessTokenLifeTime);
        user.AccessTokens.Add(accessToken.accessToken);
        
        if (NeedRefreshToken(model.grant_type))
        {
            var refreshToken = _tokenService.GenerateRefreshToken(user, clientId, new TimeSpan(7,0,0,0));
            refreshTokenValue = refreshToken.value;
            user.RefreshTokens.Add(refreshToken.refreshToken);
        }
        
        if (scopes.Contains("openid"))
        {
            var idToken = _tokenService.GenerateIdToken(user, clientId, new TimeSpan(0, 0, 2, 0, 0), nonce);
            user.IdTokens.Add(idToken.idToken);
            idTokenValue = idToken.value;
        }
        
        await _authClientRepository.SaveChangesAsync();
        
        return Result.Ok<TokenResultModel>(new()
        {
            access_token = accessToken.value,
            id_token = idTokenValue,
            refresh_token = refreshTokenValue,
            token_type = "jwt",
            expires_in = accessTokenLifeTime.Seconds
        });
    }
    
    private bool NeedRefreshToken(string grant_type)
    {
        if (grant_type is "implicit" or "client_credentials")
            return false;
        return true;
    }
    
    private Result ValidateRequest(TokenInputModel model)
    {
        if (model.grant_type != "authorization_code")
            return Result.Fail("not_implemented");
        
        return Result.Ok();
    }
    
    private async Task<Result<AuthenticateForTokenResult>> AuthorizeRequest(TokenInputModel model, IServiceProvider serviceProvider)
    {
        Result<IStrategy> authenticatorResult = AuthorizationCodeStrategy.ChooseAuthenticator(model, serviceProvider);
        if (authenticatorResult.IsFailure)
            return Result.Fail<AuthenticateForTokenResult>(authenticatorResult.Error);
    
        Result<AuthenticateForTokenResult> authenticateResult = await authenticatorResult.Value!.Validate();
        if (authenticateResult.IsFailure)
            return Result.Fail<AuthenticateForTokenResult>(authenticateResult.Error);
        
        return Result.Ok(authenticateResult.Value!);
    }

    
}
