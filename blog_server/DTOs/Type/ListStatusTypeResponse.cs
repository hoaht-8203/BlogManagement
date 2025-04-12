using System;

namespace blog_server.DTOs.Type;

public class ListStatusTypeResponse
{
    public int StatusValue { get; set; }
    public string StatusName { get; set; } = string.Empty;
}
