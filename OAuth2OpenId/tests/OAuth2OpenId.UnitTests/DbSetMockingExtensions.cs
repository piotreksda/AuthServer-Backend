using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace OAuth2OpenId.UnitTests;

public static class DbSetMockingExtensions
{
    public static DbSet<T> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.As<IEnumerable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        return mockSet.Object;
    }
}
