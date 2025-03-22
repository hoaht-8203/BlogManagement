using System;

namespace blog_server.Sessions.Impl;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? Username => _httpContextAccessor.HttpContext?.User?.Identity?.Name;
}
