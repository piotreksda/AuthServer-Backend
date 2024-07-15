using Microsoft.EntityFrameworkCore.ChangeTracking;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Abstractions;

public interface IUserRepository : IRootRepository<User, int>
{
    public Task<bool> IsUsernameAvailableAsync(string username);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<User?> GetUserByAuthCode(string code);
}