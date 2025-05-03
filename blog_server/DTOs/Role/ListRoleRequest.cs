using System;
using blog_server.DTOs.Pagination;

namespace blog_server.DTOs.Role;

public class ListRoleRequest : PaginationRequest
{
    public string Name { get; set; } = string.Empty;
}
