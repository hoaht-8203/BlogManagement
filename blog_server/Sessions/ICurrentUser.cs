using System;

namespace blog_server.Sessions;

public interface ICurrentUser
{
    string? Username { get; }
}
