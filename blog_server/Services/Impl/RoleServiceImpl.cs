using System;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Role;
using blog_server.Exceptions;
using blog_server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog_server.Services.Impl;

public class RoleServiceImpl(
    ApplicationDbContext context,
    IMapper mapper,
    ILogger<RoleServiceImpl> logger
) : IRoleService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RoleServiceImpl> _logger = logger;

    public async Task CreateRole(CreateRoleRequest request)
    {
        var role = _mapper.Map<Role>(request);
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRole(DeleteRoleRequest request)
    {
        var role =
            await _context
                .Roles.Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == request.Id)
            ?? throw new ApiException("Role not found", StatusCodes.Status400BadRequest);

        if (role.UserRoles.Count > 0)
        {
            throw new ApiException("Role is used by users", StatusCodes.Status400BadRequest);
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedList<ListRoleResponse>> ListRoles(ListRoleRequest request)
    {
        if (!request.IsValidPagination())
        {
            throw new ApiException("Invalid pagination request", StatusCodes.Status400BadRequest);
        }

        var query = _context.Roles.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(r => r.Name == request.Name);
        }

        var roles = await query.ToListAsync();
        var response = _mapper.Map<List<ListRoleResponse>>(roles);
        return new PaginatedList<ListRoleResponse>(
            response,
            roles.Count,
            request.PageNumber,
            request.PageSize
        );
    }

    public async Task UpdateRole(UpdateRoleRequest request)
    {
        var role =
            await _context.Roles.FindAsync(request.Id)
            ?? throw new ApiException("Role not found", StatusCodes.Status400BadRequest);

        _mapper.Map(request, role);
        await _context.SaveChangesAsync();
    }
}
