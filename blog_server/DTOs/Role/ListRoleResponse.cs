using System;

namespace blog_server.DTOs.Role;

public class ListRoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TotalUsers { get; set; }
    public string CreateBy { get; set; } = string.Empty;
    public string UpdateBy { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
