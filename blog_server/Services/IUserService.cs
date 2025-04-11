using System;
using blog_server.DTOs.User;
using blog_server.Models;

namespace blog_server.Services;

public interface IUserService
{
    public Task<PaginatedList<ListUserResponse>> ListUsers(ListUserRequest request);
}
