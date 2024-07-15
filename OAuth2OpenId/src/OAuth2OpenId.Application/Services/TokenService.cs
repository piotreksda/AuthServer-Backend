using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Services.Interfaces;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Services;

public class TokenService : ITokenService
{
    private readonly IKeyManager _keyManager;

    public TokenService(IKeyManager keyManager)
    {
        _keyManager = keyManager;
    }

    public (AccessToken accessToken, string value) GenerateAccessToken(User user, Guid clientId, TimeSpan tokenLifeTime)
    {
        var key = new RsaSecurityKey(_keyManager.OpenIdKey);
        var tokenId = Guid.NewGuid();
        //todo: create this based on scopes
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64)
        };

        // Token settings
        var token = new JwtSecurityToken(
            issuer: "https://localhost:7234",
            audience: clientId.ToString(),
            claims: claims,
            expires: DateTime.Now.Add(tokenLifeTime),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256){
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            }
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenString = tokenHandler.WriteToken(token);
        var accessToken = new AccessToken(tokenId, clientId, user.Id, tokenLifeTime);

        return (accessToken, tokenString);
    }

    public (AuthorizationCode authorizationCode, string value) GenerateAuthorizationCode(User user, Guid clientId, TimeSpan tokenLifeTime, int redirectId, string scopes, string? codeChallenge, string? codeChallengeMethod, string? nonce)
    {
        string code = GenerateSecureRandomString();
        
        //todo: encode refreshToken
        var refreshToken = new AuthorizationCode(clientId, user.Id, redirectId, code, tokenLifeTime, scopes, codeChallenge, codeChallengeMethod, nonce);

        return (refreshToken, code);
    }

    public (IdToken idToken, string value) GenerateIdToken(User user, Guid clientId, TimeSpan tokenLifeTime, string? nonce)
    {
        var key = new RsaSecurityKey(_keyManager.OpenIdKey);
        var tokenId = Guid.NewGuid();
        //todo: create this based on scopes
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, "dummy@example.pl"),
            new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64)
        };

        if (nonce is not null)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Nonce, nonce));
        }

        // Token settings
        var token = new JwtSecurityToken(
            issuer: "https://localhost:7234",
            audience: clientId.ToString(),
            claims: claims,
            expires: DateTime.Now.Add(tokenLifeTime),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256){
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            }
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenString = tokenHandler.WriteToken(token);
        var idToken = new IdToken(tokenId, clientId, user.Id, tokenLifeTime);

        return (idToken, tokenString);
    }

    public (RefreshToken refreshToken, string value) GenerateRefreshToken(User user, Guid clientId, TimeSpan lifeTime)
    {
        string refreshTokenValue = GenerateSecureRandomString();
        
        //todo: encode refreshToken
        var refreshToken = new RefreshToken(clientId, user.Id, refreshTokenValue, lifeTime);

        return (refreshToken, refreshTokenValue);
    }

    private string GenerateSecureRandomString()
    {
        var randomNumber = new byte[64];
    
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}