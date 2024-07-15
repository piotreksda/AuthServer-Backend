using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.AgregateRoots;

namespace OAuth2OpenId.Application.Abstractions;

public interface IRootRepository<T, in T2> where T : AggregateRoot<T2> where T2 : struct
{
    Task<T?> GetByIdAsync(T2 id, CancellationToken cancellationToken = default);


    ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default);

    void Remove(T entity);
    
    void Remove(IEnumerable<T> entities);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    // IQueryable<T> ExposeQueryable();

    Task<bool> CheckIfExistAsync(T2 id, CancellationToken cancellationToken = default);
}