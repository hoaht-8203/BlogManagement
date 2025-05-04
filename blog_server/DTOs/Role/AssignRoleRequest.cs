using System;

namespace blog_server.DTOs.Role;

public class AssignRoleRequest
{
    public string UserId { get; set; } = string.Empty;
    public List<int> RoleIds { get; set; } = [];
}
