using System;
using AutoMapper;
using blog_server.Data;
using blog_server.DTOs.User;
using blog_server.Exceptions;
using blog_server.Helpers;
using blog_server.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class UserServiceImpl(ApplicationDbContext context, IMapper mapper) : IUserService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task CreateUser(CreateUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        user.PasswordHash = PasswordHelper.HashPassword(request.Password);

        var role =
            await _context.Roles.FirstOrDefaultAsync(r => r.Name == "USER")
            ?? throw new ApiException(
                "Default role not found",
                StatusCodes.Status500InternalServerError
            );

        user.UserRoles.Add(new UserRole { RoleId = role.Id, JoinDate = DateTime.UtcNow });

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(DeleteUserRequest request)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id)
            ?? throw new ApiException("User not found", StatusCodes.Status400BadRequest);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedList<ListUserResponse>> ListUsers(ListUserRequest request)
    {
        if (!request.IsValidPagination())
        {
            throw new ApiException("Invalid pagination request", StatusCodes.Status400BadRequest);
        }

        var query = _context
            .Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Username))
        {
            query = query.Where(u => u.Username.Contains(request.Username));
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            query = query.Where(u => u.Email.Contains(request.Email));
        }

        if (!string.IsNullOrEmpty(request.Role))
        {
            query = query.Where(u => u.UserRoles.Any(ur => ur.Role.Name == request.Role));
        }

        var users = await query.ToListAsync();
        var response = _mapper.Map<List<ListUserResponse>>(users);
        return new PaginatedList<ListUserResponse>(
            response,
            users.Count,
            request.PageNumber,
            request.PageSize
        );
    }

    public async Task UpdateUser(UpdateUserRequest request)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id)
            ?? throw new ApiException("User not found", StatusCodes.Status400BadRequest);

        _mapper.Map(request, user);

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = PasswordHelper.HashPassword(request.Password);
        }

        await _context.SaveChangesAsync();
    }
}
