using System;
using blog_server.DTOs.User;

namespace blog_server.Services;

public interface IUserService
{
    public Task<List<ListUserResponse>> ListUsers(ListUserRequest request);
}
