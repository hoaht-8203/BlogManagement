using System;

namespace blog_server.DTOs.Pagination;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public virtual bool IsValidPagination()
    {
        return PageNumber >= 1 && PageSize >= 1 && PageSize <= 100;
    }
}
