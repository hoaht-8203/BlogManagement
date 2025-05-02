using System;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Type;
using blog_server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class TypeServiceImpl(
    ApplicationDbContext context,
    IMapper mapper,
    IRedisCacheService redisCacheService
) : ITypeService
{
    public readonly ApplicationDbContext _context = context;
    public readonly IMapper _mapper = mapper;
    public readonly IRedisCacheService _redisCacheService = redisCacheService;
    public readonly string _cacheKey = "type";

    public async Task<List<ListTypeResponse>> ListType(ListTypeRequest request)
    {
        if (string.IsNullOrEmpty(request.Type) || string.IsNullOrWhiteSpace(request.Type))
        {
            throw new ApiException("Type is required", StatusCodes.Status400BadRequest);
        }

        var cacheKey = $"{_cacheKey}_{request.Type}";
        var cacheData = await _redisCacheService.GetAsync<List<ListTypeResponse>>(cacheKey);
        if (cacheData != null && request.Type != AppTypes.APP_STATUS)
        {
            return cacheData;
        }

        var response = new List<ListTypeResponse>();

        if (request.Type == AppTypes.APP_STATUS)
        {
            response = ListStatusType();
        }
        else if (request.Type == AppTypes.APP_ROLES)
        {
            response = await ListRoleType();
        }

        await _redisCacheService.SetAsync(cacheKey, response);

        return response;
    }

    private static List<ListTypeResponse> ListStatusType()
    {
        var response = new List<ListTypeResponse>();
        foreach (AppStatus status in Enum.GetValues(typeof(AppStatus)))
        {
            response.Add(
                new ListTypeResponse { Name = status.ToString(), Value = ((int)status).ToString() }
            );
        }
        return response;
    }

    private async Task<List<ListTypeResponse>> ListRoleType()
    {
        var roleList = await _context.Roles.ToListAsync();
        var response = _mapper.Map<List<ListTypeResponse>>(roleList);
        return response;
    }
}
