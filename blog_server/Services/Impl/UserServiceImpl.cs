using System;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.User;
using blog_server.Exceptions;
using blog_server.Extensions;
using blog_server.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class UserServiceImpl(ApplicationDbContext context, IMapper mapper) : IUserService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<ListUserResponse>> ListUsers(ListUserRequest request)
    {
        if (!request.IsValidPagination())
        {
            throw new ApiException("Invalid pagination request", StatusCodes.Status400BadRequest);
        }

        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(request.Username))
        {
            query = query.Where(u => u.Username != null && u.Username.Contains(request.Username));
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            query = query.Where(u => u.Email != null && u.Email.Contains(request.Email));
        }

        if (!string.IsNullOrEmpty(request.FullName))
        {
            query = query.Where(u => u.FullName != null && u.FullName.Contains(request.FullName));
        }

        if (!string.IsNullOrEmpty(request.Address))
        {
            query = query.Where(u => u.Address != null && u.Address.Contains(request.Address));
        }

        if (!string.IsNullOrEmpty(request.Phone))
        {
            query = query.Where(u => u.Phone != null && u.Phone.Contains(request.Phone));
        }

        if (request.Status != null)
        {
            query = query.Where(u => u.Status == (AppStatus)request.Status);
        }

        var totalCount = await query.CountAsync();

        var listUser = await _context
            .Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Paginate(request.PageNumber, request.PageSize)
            .ToListAsync();

        var mappedUsers = _mapper.Map<List<ListUserResponse>>(listUser);
        var result = new PaginatedList<ListUserResponse>(
            mappedUsers,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
        return result;
    }
}
