using System.Linq;
using blog_server.Models;

namespace blog_server.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        where T : BaseEntity
    {
        return query.OrderBy(x => x.CreateDate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
