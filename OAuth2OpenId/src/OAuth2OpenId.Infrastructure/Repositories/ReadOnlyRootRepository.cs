using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.AgregateRoots;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Repositories;

public class ReadOnlyRootRepository<T, T2> : IReadOnlyRootRepository<T, T2> where T : BaseEntity<T2> where T2 : struct
{
    protected ReadOnlyRootRepository(AuthDbContext context)
    {
        _context = context;
    }

    protected readonly AuthDbContext _context;
    
    public IQueryable<T> GetByIdQueryable(T2 id)
    {
        IQueryable<T> result = _context.Set<T>().AsNoTracking()
            .Where(e => e.Id.Equals(id));
        
        return result;
    }

    public async Task<T?> GetByIdAsync(T2 id, CancellationToken cancellationToken = default)
    {
        IQueryable<T> result = _context.Set<T>().AsNoTracking()
            .Where(e => e.Id.Equals(id));
        
        return await result.SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<bool> CheckIfExistAsync(T2 id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public IQueryable<T> ExposeQueryable()
    {
        return _context.Set<T>().AsNoTracking().AsQueryable();
    }
}