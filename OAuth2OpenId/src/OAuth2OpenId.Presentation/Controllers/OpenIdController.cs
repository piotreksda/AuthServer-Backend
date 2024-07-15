using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Queries.UserInfo;
using OAuth2OpenId.Domain;

namespace OAuth2OpenIdPresentation.Controllers;

[ApiController]
public class OpenIdController : ControllerBase
{
    private readonly IMediator _mediator;
    private IKeyManager _keyManager;

    public OpenIdController(IMediator mediator, IKeyManager keyManager)
    {
        _mediator = mediator;
        _keyManager = keyManager;
    }
    
    [Authorize("oauth")]
    [HttpGet("/userinfo")]
    public async Task<IActionResult> UserInfo()
    {
        Guid tokenId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

        GetUserInfoQuery query = new GetUserInfoQuery(tokenId);

        var xd = new
        {
            sub = "2",
            name = "test person",
            email = "test@example.com",
            birthdate = "1975-12-31",
        };

        return Ok(xd);
        // This endpoint provides user information to the client.
        // Verify the access token and return the user's information based on the scopes granted.
    }

    [HttpGet("/.well-known/openid-configuration")]
    public async Task<IActionResult> OpenIdConfiguration()
    {
        var openIdConfiguration = new
        {
            issuer = "https://localhost:7234",
            authorization_endpoint = "https://localhost:7234/authorize",
            device_authorization_endpoint = "https://localhost:7234/device/code",
            token_endpoint = "https://localhost:7234/token",
            userinfo_endpoint = "https://localhost:7234/userinfo",
            revocation_endpoint = "https://localhost:7234/revoke",
            jwks_uri = "https://localhost:7234/.well-known/jwks.json",
            response_types_supported = new[]
            {
                "code",
                "token",
                "id_token",
                "code token",
                "code id_token",
                "token id_token",
                "code token id_token",
                "none"
            },
            subject_types_supported = new[]
            {
                "public"
            },
            id_token_signing_alg_values_supported = new[]
            {
                "RS256",
                "RSA"
            },
            scopes_supported = ConstValues.SupportedScopes,
            token_endpoint_auth_methods_supported = new[]
            {
                "client_secret_post",
                // "client_secret_basic"
            },
            claims_supported = new[]
            {
                "aud",
                "email",
                "email_verified",
                "exp",
                "family_name",
                "given_name",
                "iat",
                "iss",
                "name",
                "picture",
                "sub"
            },
            code_challenge_methods_supported = new[]
            {
                "RSH256"
            },
            grant_types_supported = new[]
            {
                "authorization_code",
                "refresh_token",
                "urn:ietf:params:oauth:grant-type:device_code",
                "urn:ietf:params:oauth:grant-type:jwt-bearer"
            }
        };

        return Ok(openIdConfiguration);
    }

    [HttpGet("/.well-known/jwks.json")]
    public async Task<IActionResult> Jwks()
    {
        RsaSecurityKey publicKey1 = new(_keyManager.OpenIdKey.ExportParameters(false));

        JsonWebKey jwk1 = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicKey1);

        var jwks = new
        {
            keys = new List<object>
            {
                jwk1
            }
        };

        return Ok(jwks);
    }

    [HttpPost("/register")]
    public async Task Register()
    {
        //Dynamic client registration endpoint
        
    }

    

    [HttpPost("logout")]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync();
    }
}