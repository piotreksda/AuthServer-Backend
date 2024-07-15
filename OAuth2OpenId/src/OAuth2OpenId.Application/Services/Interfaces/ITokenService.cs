using OAuth2OpenId.Application.Commands.Token;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Services.Interfaces;

public interface ITokenService
{
    (AccessToken accessToken, string value) GenerateAccessToken(User user, Guid clientId, TimeSpan tokenLifeTime);
    //client, user, redirectUriId, code, tokenLifeTime, request.scope, request.code_challenge, request.code_challenge_method, request.nonce
    (AuthorizationCode authorizationCode, string value) GenerateAuthorizationCode(User user, Guid clientId, TimeSpan tokenLifeTime, int redirectId, string scopes, string? codeChallenge, string? codeChallengeMethod, string? nonce);

    (IdToken idToken, string value) GenerateIdToken(User user, Guid clientId, TimeSpan tokenLifeTime,
        string? nonce);

    (RefreshToken refreshToken, string value) GenerateRefreshToken(User user, Guid clientId, TimeSpan lifeTime);
}