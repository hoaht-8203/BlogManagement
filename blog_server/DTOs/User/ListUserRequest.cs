using System;
using blog_server.Constants;
using blog_server.DTOs.Pagination;

namespace blog_server.DTOs.User;

public class ListUserRequest : PaginationRequest
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public AppStatus? Status { get; set; }
    public List<AppRole>? Roles { get; set; }
}
