using System;

namespace blog_server.DTOs.Type;

public class ListRoleTypeResponse
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string Description { get; set; } = string.Empty;
}
