using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Services.Interfaces;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;
using static System.Int32;

namespace OAuth2OpenId.Application.Commands.Authorize;

public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, ActionResult>
{
    private readonly IClientRepository _authClientRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly HttpContext? _httpContext;

    public AuthorizeCommandHandler(
        IClientRepository authClientRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository, ITokenService tokenService)
    {
        _authClientRepository = authClientRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<ActionResult> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(cancellationToken);

        if (user is null)
            return RedirectToLogin();

        var client = await GetClientAsync(request.Request.client_id, cancellationToken);

        if (client is null)
            return RedirectOnError("invalid_client", request.Request.redirect_uri);

        var requestValidation = ValidateRequest(request.Request);
        if (requestValidation.IsFailure)
            return RedirectOnError(requestValidation.Error, request.Request.redirect_uri);
        
        var scopes = ParseScopes(request.Request.scope);

        var validateClientResult = ValidateClientAsync(client, request.Request.redirect_uri, scopes);
        if (validateClientResult.IsFailure)
            return RedirectOnError(validateClientResult.Error, request.Request.redirect_uri);

        var accessPermitResult = CheckUserScopesPermitsAsync(client, user, scopes);
        if (accessPermitResult.IsFailure)
            return RedirectOnPermissionRequired(request.Request.client_id, scopes);

        AuthorizeResult authorizeResult = new AuthorizeResult();
        if (request.Request.response_type.Contains("code"))
        {
            var code = _tokenService.GenerateAuthorizationCode(user, client.Id, new TimeSpan(0, 1, 0, 0, 0),
                validateClientResult.Value!.RedirectUrlId, request.Request.scope ,request.Request.code_challenge,
                request.Request.code_challenge_method, request.Request.nonce);
            user.AuthorizationCodes.Add(code.authorizationCode);
            authorizeResult.Code = code.value;
        }

        if (request.Request.response_type.Contains("token"))
        {
            var accessToken = _tokenService.GenerateAccessToken(user, client.Id, new TimeSpan(0, 1, 0, 0, 0));
            user.AccessTokens.Add(accessToken.accessToken);
            authorizeResult.Token = accessToken.value;
        }

        if (request.Request.response_type.Contains("id_token"))
        {
            var tokenLifeTime = new TimeSpan(0, 1, 0, 0, 0);
            var idToken = _tokenService.GenerateIdToken(user, client.Id, tokenLifeTime, request.Request.nonce);
            user.IdTokens.Add(idToken.idToken);
            authorizeResult.IdToken = idToken.value;
            authorizeResult.expires_in = tokenLifeTime.Seconds;
        }
        
        await _authClientRepository.SaveChangesAsync(cancellationToken);

        authorizeResult.State = request.Request.state;

        return RedirectWithAuthorizationCode(request.Request.redirect_uri, authorizeResult);
    }

    private async Task<User?> GetUserAsync(CancellationToken cancellationToken)
    {
        if (!IsUserAuthenticated())
            return null;
        return await _userRepository.GetByIdAsync(GetUserId(), cancellationToken: cancellationToken);
    }

    private async Task<Client?> GetClientAsync(Guid clientId, CancellationToken cancellationToken)
    {
        return await _authClientRepository.GetByIdAsync(clientId, cancellationToken);
    }

    private int GetUserId()
    {
        return Parse(_httpContext!.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }

    private bool IsUserAuthenticated()
    {
        return _httpContext?.User.Identity?.IsAuthenticated ?? false;
    }

    private RedirectResult RedirectToLogin()
    {
        var continueEncoded = System.Web.HttpUtility.UrlEncode(_httpContext!.Request.GetEncodedUrl());
        return new RedirectResult($"https://localhost:5173/Login?continue={continueEncoded}");
    }

    private Result<ValidateClientViewModel> ValidateClientAsync(Client client, string redirectUri, List<string> scopes)
    {
        var redirectUriEntity = client.ClientEnvironments.SelectMany(x => x.RedirectUris).SingleOrDefault(x => x.Uri.Equals(redirectUri));

        if (redirectUriEntity is null)
            return Result.Fail<ValidateClientViewModel>("redirect_uri_mismatch");

        if (!scopes.All(scope => client.AllowedScopes.Select(x => x.Name).Contains(scope)))
            return Result.Fail<ValidateClientViewModel>("invalid_scope");

        return Result.Ok(new ValidateClientViewModel { RedirectUrlId = redirectUriEntity.Id });
    }

    private Result CheckUserScopesPermitsAsync(Client client, User user, List<string> scopes)
    {
        //todo: optimize
        var alreadyPermittedScopes = user.UserPermits.Select(x => x.ClientScope).Where(x => x.ClientId == client.Id);
        var allPermits = 
            scopes.All(x => alreadyPermittedScopes.Select(x => x.Name).Contains(x));

        if (!allPermits)
            return Result.Fail("authorization_pending");

        return Result.Ok();
    }

    private Result ValidateRequest(AuthorizationRequest request)
    {
        if (string.IsNullOrEmpty(request.scope))
            return Result.Fail("invalid_scope");

        if (request.response_type != "code")
            return Result.Fail("unsupported_response_type");

        if (string.IsNullOrEmpty(request.redirect_uri))
            return Result.Fail("redirect_uri_missing");

        if ((string.IsNullOrEmpty(request.code_challenge) || string.IsNullOrEmpty(request.code_challenge_method)) &&
            (!string.IsNullOrEmpty(request.code_challenge) || !string.IsNullOrEmpty(request.code_challenge_method)))
            return Result.Fail("unauthorized_client");

        return Result.Ok();
    }

    private RedirectResult RedirectOnError(string error, string redirectUri)
    {
        var errorUri = BuildErrorQuery(error, redirectUri);
        return new RedirectResult(errorUri);
    }

    private RedirectResult RedirectOnPermissionRequired(Guid clientId, List<string> scopes)
    {
        var permitUri = BuildPermitScopesUri(clientId.ToString(), scopes);
        return new RedirectResult(permitUri);
    }

    private RedirectResult RedirectWithAuthorizationCode(string redirectUri, AuthorizeResult authorizeResult)
    {
        var uriBuilder = new UriBuilder(redirectUri);
        var query = new QueryBuilder();
        
        if (authorizeResult.State is not null)
            query.Add("state", authorizeResult.State);
        if (authorizeResult.Code is not null)
            query.Add("code", authorizeResult.Code);
        if (authorizeResult.Token is not null)
            query.Add("token", authorizeResult.Token);
        if (authorizeResult.IdToken is not null)
            query.Add("id_token", authorizeResult.IdToken);
        uriBuilder.Query = query.ToQueryString().Value;
        return new RedirectResult(uriBuilder.ToString());
    }

    private string BuildErrorQuery(string message, string redirectUri)
    {
        var url = new Uri("https://localhost:5173/");
        // var url = new Uri(_httpContext!.Request.GetDisplayUrl());
        var urlBuilder = new UriBuilder($"{url.GetLeftPart(UriPartial.Authority)}/Error");

        var query = new QueryBuilder
        {
            { "Message", message },
            { "RedirectUri", $"{redirectUri}?error={message}" }
        };

        urlBuilder.Query = query.ToQueryString().Value;

        return urlBuilder.ToString();
    }

    private string BuildPermitScopesUri(string clientId, List<string> scopes)
    {
        // var url = new Uri(_httpContext!.Request.GetDisplayUrl());
        // var urlBuilder = new UriBuilder($"{url.GetLeftPart(UriPartial.Authority)}/Permit");
        var urlBuilder = new UriBuilder($"https://localhost:5173/Permit");

        var query = new QueryBuilder
        {
            { "ClientId", clientId },
            { "Continue", System.Web.HttpUtility.UrlEncode(_httpContext!.Request.GetEncodedUrl()) }
        };

        foreach (var scope in scopes)
        {
            query.Add("scopes", scope);
        }

        urlBuilder.Query = query.ToQueryString().Value;

        return urlBuilder.ToString();
    }

    private List<string> ParseScopes(string scopes)
    {
        return scopes.Split(" ").ToList();
    }
    

}
