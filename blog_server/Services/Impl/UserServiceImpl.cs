using System;
using AutoMapper;
using blog_server.Data;
using blog_server.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class UserServiceImpl(ApplicationDbContext context, IMapper mapper) : IUserService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ListUserResponse>> ListUsers(ListUserRequest request)
    {
        var listUser = await _context
            .Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        var result = _mapper.Map<List<ListUserResponse>>(listUser);
        return result;
    }
}
