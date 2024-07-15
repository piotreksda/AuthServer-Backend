using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static async Task<T?> GetById<T, T2>(this IQueryable<T> query, T2 id)
        where T : BaseEntity<T2> where T2 : struct
    {
        return await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
    }
}