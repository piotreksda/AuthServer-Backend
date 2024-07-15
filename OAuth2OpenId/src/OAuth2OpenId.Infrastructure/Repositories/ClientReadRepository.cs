using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities.EntitiesDtos;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Repositories;

public class ClientReadRepository : IClientReadRepository
{
    private readonly AuthReadOnlyDbContext _context;

    public ClientReadRepository(AuthReadOnlyDbContext context)
    {
        _context = context;
    }

    public Task<List<ClientDto>> GetClients(CancellationToken cancellationToken = default)
    {
        return _context.Clients.ToListAsync(cancellationToken);
    }
}