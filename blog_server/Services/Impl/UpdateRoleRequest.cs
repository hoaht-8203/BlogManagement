using System;

namespace blog_server.Services.Impl;

public class UpdateRoleRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
