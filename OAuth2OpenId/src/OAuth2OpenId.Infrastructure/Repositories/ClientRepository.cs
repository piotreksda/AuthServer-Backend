using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Repositories;

public class ClientRepository : RootRepository<Client, Guid>, IClientRepository
{
    public ClientRepository(AuthDbContext context) : base(context)
    {
        
    }
    
    public override Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Clients.Where(x => x.Id == id)
            .Include(c => c.ClientEnvironments)
            .ThenInclude(ce => ce.RedirectUris)
            .Include(c => c.AllowedScopes)
            .Include(c => c.AllowedGrantTypes)
            .SingleOrDefaultAsync(cancellationToken);

    }

    // public async Task<List<ClientListViewModel>> GetClientList(int userId)
    // {
    //     return await _context.Clients.Where(x => x.OwnerId == userId)
    //         .Select(x => new ClientListViewModel()
    //         {
    //             Id = x.Id,
    //             Name = x.Name,
    //             IsActive = x.IsActive
    //         }).ToListAsync();
    // }
    //
    // public async Task<ClientDetailViewModel?> GetClientDetail(int userId, Guid clientId)
    // {
    //     return await _context.Clients.Where(x => x.OwnerId == userId && x.Id == clientId)
    //         .Select(x => new ClientDetailViewModel()
    //         {
    //             ClientId = x.Id,
    //             IsActive = x.IsActive,
    //             Name = x.Name,
    //             ClientEnvironments = x.ClientEnvironments.Select(x => new ClientEnvironmentListViewModel()
    //             {
    //                 Id = x.Id,
    //                 Name = x.Name
    //             }).ToList(),
    //             GrantTypes = x.AllowedGrantTypes.Select(x => new GrantTypeListViewModel()
    //             {
    //                 Id = x.Id,
    //                 Name = x.Name
    //             }).ToList(),
    //             Scopes = x.AllowedScopes.Select(x => new ScopesListViewModel()
    //             {
    //                 Id = x.Id,
    //                 Name = x.Name
    //             }).ToList()
    //         }).SingleOrDefaultAsync();
    // }
    //
    // public async Task<Client?> GetByIdAsync(Guid clientId, bool toEdit = false, CancellationToken cancellationToken = default)
    // {
    //     return await _context.Clients
    //         .Include(x => x.AllowedScopes).ThenInclude(x => x.UserPermits)
    //         .Include(x => x.ClientEnvironments).ThenInclude(x => x.RedirectUris)
    //         .Include(x => x.AllowedGrantTypes)
    //         .SingleOrDefaultAsync(x => x.Id == clientId);
    // }
    //
    // public async Task<Result<Client>> GetClient(Guid clientId)
    // {
    //     var client = await _context.Clients
    //         .Include(x => x.ClientEnvironments)
    //             .ThenInclude(x => x.RedirectUris)
    //         .Include(x => x.AllowedScopes)
    //         .SingleOrDefaultAsync(x => x.Id == clientId);
    //     
    //     if (client is null)
    //         return Result.Fail<Client>("invalid_client_id");
    //
    //     return Result.Ok<Client>(client);
    // }

    // public async Task<Result> AccessPermitted(Guid clientId, int userId, List<string> scopes)
    // {
    //     Result<Client> clientResult = await GetClient(clientId);
    //
    //     if (clientResult.IsFailure)
    //     {
    //         return Result.Fail(clientResult.Error);
    //     }
    //
    //     var client = clientResult.Value!;
    //     
    //     int userScopesCount =
    //         await _context.UserPermits
    //             .Where(x => 
    //                 x.ClientScope.ClientId == client.Id 
    //                 && scopes.Contains(x.ClientScope.Name)
    //                 && x.UserId == userId)
    //             .CountAsync();
    //     
    //     if (userScopesCount != scopes.Count)
    //         return Result.Fail("authorization_pending");
    //     
    //     return Result.Ok();
    // }
    //
    // public async Task<Result<List<ClientScopeViewModel>>> GetClientScopes(Guid clientId, int userId, CancellationToken cancellationToken)
    // {
    //     var result = await _context.ClientScopes.Where(x => x.Client.Id == clientId).Select(x => new ClientScopeViewModel()
    //     {
    //         ClientScopeId = x.Id,
    //         ScopeName = x.Name,
    //         Accepted = x.UserPermits.Any(x => x.UserId == userId)
    //     }).ToListAsync(cancellationToken);
    //     
    //     return Result.Ok(result);
    // }
    //
    // public async Task<Result> ChangeUserClientScopes(int userId, List<int> scopeIds, CancellationToken cancellationToken)
    // {
    //     User? user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
    //     if (user is null)
    //         return Result.Fail("User not found");
    //     
    //     List<ClientScope> scopesToAccept = await _context.ClientScopes.Where(x => scopeIds.Contains(x.Id)).ToListAsync(cancellationToken);
    //
    //     await _context.UserPermits.Where(x => x.UserId == userId).ExecuteDeleteAsync(cancellationToken);
    //
    //     List<UserPermit> userPermits = new List<UserPermit>();
    //     
    //     foreach (var scope in scopesToAccept)
    //     {
    //         userPermits.Add(new (user, scope));
    //     }
    //
    //     await _context.UserPermits.AddRangeAsync(userPermits, cancellationToken);
    //
    //     await _context.SaveChangesAsync(cancellationToken);
    //     
    //     return Result.Ok();
    // }
    //
    // public async Task<Result> AddRefreshToken(Guid clientId, int userId, string refreshToken, TimeSpan lifeTime)
    // {
    //     Result clientExist = await ValidateClientExist(clientId);
    //
    //     if (clientExist.IsFailure)
    //         return Result.Fail<string>(clientExist.Error);
    //     
    //     Result userExist = await ValidateUserExist(userId);
    //
    //     if (userExist.IsFailure)
    //         return Result.Fail<string>(userExist.Error);
    //
    //     RefreshToken refreshTokenToAdd = new RefreshToken(clientId, userId, refreshToken, lifeTime);
    //
    //     _context.RefreshTokens.Add(refreshTokenToAdd);
    //     
    //     return Result.Ok();
    // }
    //
    // public async Task<Result> AddAuthorizationCode(Guid clientId, int userId, int redirectUriId, string code, string scope, TimeSpan lifeTime, string? challengeVerifier = null, string? challengeVerifierMethod = null)
    // {
    //     Result clientExist = await ValidateClientExist(clientId);
    //
    //     if (clientExist.IsFailure)
    //         return Result.Fail<string>(clientExist.Error);
    //     
    //     Result userExist = await ValidateUserExist(userId);
    //
    //     if (userExist.IsFailure)
    //         return Result.Fail<string>(userExist.Error);
    //
    //     AuthorizationCode authorizationCodeToAdd = new AuthorizationCode(clientId, userId, redirectUriId, code,
    //         lifeTime, scope, challengeVerifier, challengeVerifierMethod);
    //
    //     await _context.AuthorizationCodes.AddAsync(authorizationCodeToAdd);
    //     
    //     return Result.Ok();
    // }
    //
    // public async Task<Result> AddAccessToken(Guid jwi, Guid clientId, int userId, TimeSpan lifeTime)
    // {
    //     Result clientExist = await ValidateClientExist(clientId);
    //
    //     if (clientExist.IsFailure)
    //         return Result.Fail<string>(clientExist.Error);
    //     
    //     Result userExist = await ValidateUserExist(userId);
    //
    //     if (userExist.IsFailure)
    //         return Result.Fail<string>(userExist.Error);
    //
    //     AccessToken accessTokenToAdd = new AccessToken(jwi, clientId, userId, lifeTime);
    //
    //     _context.AccessTokens.Add(accessTokenToAdd);
    //     
    //     return Result.Ok();
    // }
    //
    // private async Task<Result> ValidateClientExist(Guid clientId)
    // {
    //     bool exist = await _context.Clients.AnyAsync(x => x.Id == clientId);
    //     if (!exist)
    //         return Result.Fail("User not found");
    //     
    //     return Result.Ok();
    // }
    //
    // private async Task<Result> ValidateUserExist(int userId)
    // {
    //     bool exist = await _context.Users.AnyAsync(x => x.Id == userId);
    //     if (!exist)
    //         return Result.Fail("User not found");
    //     
    //     return Result.Ok();
    // }
}



// using Microsoft.EntityFrameworkCore;
// using OAuth2OpenId.Application.Abstractions;
// using OAuth2OpenId.Domain.Entities;
// using OAuth2OpenId.Domain.Models;
// using OAuth2OpenId.Infrastructure.EntityFramework;
//
// namespace OAuth2OpenId.Infrastructure.Repositories;
//
// public class OAuthClientRepository : IOAuthClientRepository
// {
//     private readonly OAuth2OpenIdDbContext _context;
//
//     public OAuthClientRepository(OAuth2OpenIdDbContext context)
//     {
//         _context = context;
//     }
//
//     public async Task<Result<ValidateClientViewModel>> ValidateClient(Guid clientId, string redirectUrl, List<string> scopes)
//     {
//         Result<Client> clientResult = await GetClient(clientId);
//
//         if (clientResult.IsFailure)
//         {
//             return Result.Fail<ValidateClientViewModel>(clientResult.Error);
//         }
//
//         Client client = clientResult.Value!;
//
//         RedirectUri? redirectUri = client.RedirectUris.SingleOrDefault(x => x.Uri.Equals(redirectUrl));
//         
//         if (redirectUri is null)
//             return Result.Fail<ValidateClientViewModel>("redirect_uri_mismatch");
//         
//         if (!scopes.All(scope => client.AllowedScopes.Select(x => x.Name).Contains(scope)))
//             return Result.Fail<ValidateClientViewModel>("invalid_scope");
//         
//         return Result.Ok(new ValidateClientViewModel()
//         {
//             RedirectUrlId = redirectUri.Id
//         });
//     }
//
//     public async Task<Result> AccessPermitted(Guid clientId, int userId, List<string> scopes)
//     {
//         Result<Client> clientResult = await GetClient(clientId);
//
//         if (clientResult.IsFailure)
//         {
//             return Result.Fail(clientResult.Error);
//         }
//
//         var client = clientResult.Value!;
//         
//         int userScopesCount =
//             await _context.UserPermits
//                 .Where(x => 
//                     x.ClientScope.ClientId == client.Id 
//                     && scopes.Contains(x.ClientScope.Name)
//                     && x.UserId == userId)
//                 .CountAsync();
//         
//         if (userScopesCount != scopes.Count)
//             return Result.Fail("authorization_pending");
//         
//         return Result.Ok();
//     }
//
//     public async Task<Result<List<ClientScopeViewModel>>> GetClientScopes(Guid clientId, int userId, CancellationToken cancellationToken)
//     {
//         var result = await _context.ClientScopes.Where(x => x.Client.Id == clientId).Select(x => new ClientScopeViewModel()
//         {
//             ClientScopeId = x.Id,
//             ScopeName = x.Name,
//             Accepted = x.UserPermits.Any(x => x.UserId == userId)
//         }).ToListAsync(cancellationToken);
//         
//         return Result.Ok(result);
//     }
//
//     public async Task<Result> ChangeUserClientScopes(int userId, List<int> scopeIds, CancellationToken cancellationToken)
//     {
//         User? user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
//         if (user is null)
//             return Result.Fail("User not found");
//         
//         List<ClientScope> scopesToAccept = await _context.ClientScopes.Where(x => scopeIds.Contains(x.Id)).ToListAsync(cancellationToken);
//
//         await _context.UserPermits.Where(x => x.UserId == userId).ExecuteDeleteAsync(cancellationToken);
//
//         List<UserPermit> userPermits = new List<UserPermit>();
//         
//         foreach (var scope in scopesToAccept)
//         {
//             userPermits.Add(new (user, scope));
//         }
//
//         await _context.UserPermits.AddRangeAsync(userPermits, cancellationToken);
//
//         await _context.SaveChangesAsync(cancellationToken);
//         
//         return Result.Ok();
//     }
//
//     private async Task<Result<Client>> GetClient(Guid clientId)
//     {
//         var client = await _context.Clients
//             .Include(x => x.RedirectUris)
//             .Include(x => x.AllowedScopes)
//             .SingleOrDefaultAsync(x => x.Id == clientId);
//
//         if (client is null)
//             return Result.Fail<Client>("invalid_client_id");
//
//         return Result.Ok(client);
//     }
//
//     public async Task<Result> AddRefreshToken(Guid clientId, int userId, string refreshToken, TimeSpan lifeTime)
//     {
//         Result clientExist = await ValidateClientExist(clientId);
//
//         if (clientExist.IsFailure)
//             return Result.Fail<string>(clientExist.Error);
//         
//         Result userExist = await ValidateUserExist(userId);
//
//         if (userExist.IsFailure)
//             return Result.Fail<string>(userExist.Error);
//
//         RefreshToken refreshTokenToAdd = new RefreshToken(clientId, userId, refreshToken, lifeTime);
//
//         _context.RefreshTokens.Add(refreshTokenToAdd);
//         
//         return Result.Ok();
//     }
//
//     public async Task<Result> AddAuthorizationCode(Guid clientId, int userId, int redirectUriId, string code, string scope, TimeSpan lifeTime, string? challengeVerifier = null, string? challengeVerifierMethod = null)
//     {
//         Result clientExist = await ValidateClientExist(clientId);
//
//         if (clientExist.IsFailure)
//             return Result.Fail<string>(clientExist.Error);
//         
//         Result userExist = await ValidateUserExist(userId);
//
//         if (userExist.IsFailure)
//             return Result.Fail<string>(userExist.Error);
//
//         AuthorizationCode authorizationCodeToAdd = new AuthorizationCode(clientId, userId, redirectUriId, code,
//             lifeTime, scope, challengeVerifier, challengeVerifierMethod);
//
//         await _context.AuthorizationCodes.AddAsync(authorizationCodeToAdd);
//         
//         return Result.Ok();
//     }
//     
//     public async Task<Result> AddAccessToken(Guid jwi, Guid clientId, int userId, TimeSpan lifeTime)
//     {
//         Result clientExist = await ValidateClientExist(clientId);
//
//         if (clientExist.IsFailure)
//             return Result.Fail<string>(clientExist.Error);
//         
//         Result userExist = await ValidateUserExist(userId);
//
//         if (userExist.IsFailure)
//             return Result.Fail<string>(userExist.Error);
//
//         AccessToken accessTokenToAdd = new AccessToken(jwi, clientId, userId, lifeTime);
//
//         _context.AccessTokens.Add(accessTokenToAdd);
//         
//         return Result.Ok();
//     }
//
//     private async Task<Result> ValidateClientExist(Guid clientId)
//     {
//         bool exist = await _context.Clients.AnyAsync(x => x.Id == clientId);
//         if (!exist)
//             return Result.Fail("User not found");
//         
//         return Result.Ok();
//     }
//     
//     private async Task<Result> ValidateUserExist(int userId)
//     {
//         bool exist = await _context.Users.AnyAsync(x => x.Id == userId);
//         if (!exist)
//             return Result.Fail("User not found");
//         
//         return Result.Ok();
//     }
//     
//     public async Task SaveChangeAsync()
//     {
//         await _context.SaveChangesAsync();
//     }
//     
// }