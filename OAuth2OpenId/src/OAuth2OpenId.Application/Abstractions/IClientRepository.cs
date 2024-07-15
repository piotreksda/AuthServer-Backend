using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Abstractions;

public interface IClientRepository : IRootRepository<Client, Guid>
{
    // public new Task<Client?> GetByIdAsync(Guid clientId, bool toEdit = false, CancellationToken cancellationToken = default);
    // public Task<Result<Client>> GetClient(Guid clientId);
    //
    // public Task<List<ClientListViewModel>> GetClientList(int userId);
    // public Task<ClientDetailViewModel?> GetClientDetail(int userId, Guid clientId);
    // public Task<Result<List<ClientScopeViewModel>>> GetClientScopes(Guid clientId, int userId,
    //     CancellationToken cancellationToken);
}