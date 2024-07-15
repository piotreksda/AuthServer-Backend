using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.AgregateRoots;
using OAuth2OpenId.Infrastructure.EntityFramework;

namespace OAuth2OpenId.Infrastructure.Repositories;

public abstract class RootRepository<T, T2> : IRootRepository<T, T2> where T : AggregateRoot<T2> where T2 : struct
{
    protected RootRepository(AuthDbContext context)
    {
        _context = context;
    }

    protected readonly AuthDbContext _context;

    abstract public Task<T?> GetByIdAsync(T2 id, CancellationToken cancellationToken = default);

    public ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    
    public void Remove(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> CheckIfExistAsync(T2 id, CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }
}