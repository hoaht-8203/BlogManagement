using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Category;
using blog_server.Exceptions;
using blog_server.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class CategoryServiceImpl : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCache;
    private const string CACHE_KEY_PREFIX = "category:";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(30);

    public CategoryServiceImpl(
        ApplicationDbContext context,
        IMapper mapper,
        IRedisCacheService redisCache
    )
    {
        _context = context;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request)
    {
        if (request.ParentId.HasValue)
        {
            var parentExists = await _context.Categories.AnyAsync(c =>
                c.Id == request.ParentId && c.Status == AppStatus.Active
            );

            if (!parentExists)
            {
                throw new ApiException(
                    $"Parent category with ID {request.ParentId} not found or inactive",
                    StatusCodes.Status400BadRequest
                );
            }
        }

        var category = _mapper.Map<Category>(request);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        // Invalidate related caches
        await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}list");
        if (request.ParentId.HasValue)
        {
            await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}{request.ParentId}");
        }

        return _mapper.Map<CreateCategoryResponse>(category);
    }

    public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest request)
    {
        var updateCategory =
            await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id)
            ?? throw new ApiException(
                $"Category with ID {request.Id} not found",
                StatusCodes.Status400BadRequest
            );

        _mapper.Map(request, updateCategory);
        await _context.SaveChangesAsync();

        // Invalidate related caches
        await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}{request.Id}");
        await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}list");
        if (updateCategory.ParentId.HasValue)
        {
            await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}{updateCategory.ParentId}");
        }

        return _mapper.Map<UpdateCategoryResponse>(updateCategory);
    }

    public async Task<DeleteCategoryResponse> DeleteCategory(DeleteCategoryRequest request)
    {
        var delCategory =
            await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.CategoryId)
            ?? throw new ApiException(
                $"Category with ID {request.CategoryId} not found",
                StatusCodes.Status400BadRequest
            );

        delCategory.Status = AppStatus.Deleted;
        await _context.SaveChangesAsync();

        // Invalidate related caches
        await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}{request.CategoryId}");
        await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}list");
        if (delCategory.ParentId.HasValue)
        {
            await _redisCache.RemoveAsync($"{CACHE_KEY_PREFIX}{delCategory.ParentId}");
        }

        return new DeleteCategoryResponse() { Id = delCategory.Id };
    }

    public async Task<DetailCategoryResponse> DetailCategory(DetailCategoryRequest request)
    {
        var cacheKey = $"{CACHE_KEY_PREFIX}{request.CategoryId}";
        System.Console.WriteLine("GO TO HERE 1");

        try
        {
            var cachedCategory = await _redisCache.GetAsync<DetailCategoryResponse>(cacheKey);
            System.Console.WriteLine("GO TO HERE 2");

            if (cachedCategory != null)
            {
                System.Console.WriteLine("Get from cache");
                return cachedCategory;
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Cache error: {ex.Message}");
            // Continue to get from database if cache fails
        }

        var detailCategory =
            await _context
                .Categories.Include(c => c.Children)
                .SingleOrDefaultAsync(c => c.Id == request.CategoryId)
            ?? throw new ApiException(
                $"Category with ID {request.CategoryId} not found",
                StatusCodes.Status400BadRequest
            );

        var response = _mapper.Map<DetailCategoryResponse>(detailCategory);
        System.Console.WriteLine("Get from db");

        try
        {
            await _redisCache.SetAsync(cacheKey, response, CACHE_DURATION);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Cache set error: {ex.Message}");
            // Continue even if cache fails
        }

        return response;
    }

    public async Task<ListCategoryResponse> ListCategories(ListCategoryRequest request)
    {
        const string cacheKey = $"{CACHE_KEY_PREFIX}list";
        var cachedList = await _redisCache.GetAsync<ListCategoryResponse>(cacheKey);

        if (cachedList != null)
        {
            return cachedList;
        }

        var rootCategories = await _context
            .Categories.Include(c => c.Children)
            .Where(c => c.ParentId == null && c.Status == AppStatus.Active)
            .ToListAsync();

        var response = new ListCategoryResponse
        {
            Categories = rootCategories.Select(c => _mapper.Map<ParentCategoryDto>(c)).ToList(),
        };

        foreach (var parentCategory in response.Categories)
        {
            var parent = rootCategories.First(c => c.Id == parentCategory.Id);
            parentCategory.Children = parent
                .Children.Where(c => c.Status == AppStatus.Active)
                .Select(c => _mapper.Map<ChildCategoryDto>(c))
                .ToList();
        }

        await _redisCache.SetAsync(cacheKey, response, CACHE_DURATION);
        return response;
    }
}
