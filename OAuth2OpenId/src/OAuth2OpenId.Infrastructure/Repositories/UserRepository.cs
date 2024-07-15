using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Repositories;

public class UserRepository : RootRepository<User, int>, IUserRepository
{
    public UserRepository(AuthDbContext context) : base(context)
    {
        
    }
    
    public override Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Users.Where(x => x.Id == id)
            .Include(u => u.AccessTokens)
            .Include(u => u.IdTokens)
            .Include(u => u.AuthorizationCodes)
            .Include(u => u.RefreshTokens)
            .Include(u => u.UserPermits)
                .ThenInclude(u => u.ClientScope)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> IsUsernameAvailableAsync(string username)
    {
        return _context.Users.AllAsync(u => u.Username != username);
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return _context.Users
            .Include(u => u.AccessTokens)
            .Include(u => u.IdTokens)
            .Include(u => u.AuthorizationCodes)
            .Include(u => u.RefreshTokens)
            .Include(u => u.UserPermits)
            .ThenInclude(u => u.ClientScope)
            .SingleOrDefaultAsync(u => u.Username == username);
    }

    public Task<User?> GetUserByAuthCode(string code)
    {
        return _context.Users
            .Include(u => u.AccessTokens)
            .Include(u => u.IdTokens)
            .Include(u => u.AuthorizationCodes)
            .Include(u => u.RefreshTokens)
            .Include(u => u.UserPermits)
            .ThenInclude(u => u.ClientScope)
            .SingleOrDefaultAsync(x =>
            x.AuthorizationCodes.Any(x => x.Code == code && x.IsUsed == false));
    }
}