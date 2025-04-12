using System;
using blog_server.DTOs.Type;

namespace blog_server.Services;

public interface ITypeService
{
    public Task<List<object>> ListType(ListTypeRequest request);
}
