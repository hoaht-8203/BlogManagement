using System;
using blog_server.DTOs.Role;
using blog_server.Models;
using blog_server.Services.Impl;

namespace blog_server.Services;

public interface IRoleService
{
    public Task<PaginatedList<ListRoleResponse>> ListRoles(ListRoleRequest request);
    public Task CreateRole(CreateRoleRequest request);
    public Task UpdateRole(UpdateRoleRequest request);
    public Task DeleteRole(DeleteRoleRequest request);
}
