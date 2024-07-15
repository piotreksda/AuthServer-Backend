using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> GetWithIncludes<T>(this IQueryable<T> query, params Expression<Func<T, object>>[]? includes)
        where T : class
    {
        if (includes is not null)
        {
            query = includes.Aggregate(query, 
                (current, include) => current.Include(include));
        }

        return query;
    }
}