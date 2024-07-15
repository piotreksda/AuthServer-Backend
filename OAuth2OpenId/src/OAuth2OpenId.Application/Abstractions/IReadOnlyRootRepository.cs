using System.Linq.Expressions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.AgregateRoots;

namespace OAuth2OpenId.Application.Abstractions;

//todo: change to dto only
public interface IReadOnlyRootRepository<T, in T2> where T : BaseEntity<T2> where T2 : struct
{
    IQueryable<T> GetByIdQueryable(T2 id);
    Task<T?> GetByIdAsync(T2 id, CancellationToken cancellationToken = default);
    IQueryable<T> ExposeQueryable();
    Task<bool> CheckIfExistAsync(T2 id, CancellationToken cancellationToken = default);
}