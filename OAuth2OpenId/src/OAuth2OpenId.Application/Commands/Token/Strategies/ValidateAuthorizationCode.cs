using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Token.Strategies;

public class ValidateAuthorizationCode : IStrategy
{
    private TokenInputModel model { get; init; }
    // private readonly IAuthorizationCodeRepository _authorizationCodeRepository;
    private readonly IUserRepository _userRepository;
    public ValidateAuthorizationCode(TokenInputModel model, IUserRepository userRepository)
    {
        this.model = model;
        _userRepository = userRepository;
    }

    public async Task<Result<AuthenticateForTokenResult>> Validate()
    {
        string? code_verifier = null;
        if (model.code_verifier is not null)
        {
            code_verifier = ReGenerateCodeChallenge(model.code_verifier);
        }

        // throw new NotImplementedException();
        User? user = await _userRepository.GetUserByAuthCode(model.code);
        // AuthorizationCode? authCode = await _authorizationCodeRepository.GetActiveByCodeHash(model.code, model.redirect_uri, code_verifier);
        
        if (user is null)
            return Result.Fail<AuthenticateForTokenResult>("invalid_grant");

        AuthorizationCode? authCode = user.AuthorizationCodes.SingleOrDefault(x => x.Code == model.code);
        
        if (authCode is null)
            return Result.Fail<AuthenticateForTokenResult>("invalid_grant");
        
        if (authCode.ChallengeVerifier != code_verifier)
            return Result.Fail<AuthenticateForTokenResult>("invalid_grant");
        
        return Result.Ok( new AuthenticateForTokenResult
        {
            User = user,
            ClientId = authCode.ClientId,
            Scopes = authCode.Scope,
            Nonce = authCode.Nonce
        });
    }
    
    private static string ReGenerateCodeChallenge(string codeVerifier)
    {
        // using (SHA256 sha256 = SHA256.Create())
        // {
        //     byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
        //     return Convert.ToBase64String(hash);
        // }
        return WebEncoders.Base64UrlEncode(SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier)));
    }
}