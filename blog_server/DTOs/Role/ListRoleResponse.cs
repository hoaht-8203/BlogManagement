using System;

namespace blog_server.DTOs.Role;

public class ListRoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
