using System;
using System.Linq;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Role;
using blog_server.Exceptions;
using blog_server.Extensions;
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

    public async Task AssignRole(AssignRoleRequest request)
    {
        var user =
            await _context
                .Users.Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id.ToString() == request.UserId)
            ?? throw new ApiException("User not found", StatusCodes.Status400BadRequest);

        var roles = await _context.Roles.Where(r => request.RoleIds.Contains(r.Id)).ToListAsync();

        if (roles.Count != request.RoleIds.Count)
        {
            throw new ApiException("One or more roles not found", StatusCodes.Status400BadRequest);
        }

        var newUserRoles = roles
            .Select(role => new UserRole { RoleId = role.Id, UserId = user.Id })
            .ToList();

        user.UserRoles.Clear();

        await _context.UserRoles.AddRangeAsync(newUserRoles);
        await _context.SaveChangesAsync();
    }

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

        var query = _context.Roles.Include(r => r.UserRoles).AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(r => r.Name == request.Name);
        }

        var totalCount = await query.CountAsync();
        var roles = await query.Paginate(request.PageNumber, request.PageSize).ToListAsync();
        var response = _mapper.Map<List<ListRoleResponse>>(roles);

        return new PaginatedList<ListRoleResponse>(
            response,
            request.PageNumber,
            request.PageSize,
            totalCount
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
