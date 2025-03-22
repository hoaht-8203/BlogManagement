using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Category;
using blog_server.Exceptions;
using blog_server.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Services.Impl;

public class CategoryServiceImpl(ApplicationDbContext context, IMapper mapper) : ICategoryService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

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

        throw new NotImplementedException();
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

        return new DeleteCategoryResponse() { Id = delCategory.Id };
    }

    public async Task<DetailCategoryResponse> DetailCategory(DetailCategoryRequest request)
    {
        var detailCategory =
            await _context
                .Categories.Include(c => c.Children)
                .SingleOrDefaultAsync(c => c.Id == request.CategoryId)
            ?? throw new ApiException(
                $"Category with ID {request.CategoryId} not found",
                StatusCodes.Status400BadRequest
            );

        return _mapper.Map<DetailCategoryResponse>(detailCategory);
    }

    public async Task<ListCategoryResponse> ListCategories(ListCategoryRequest request)
    {
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

        return response;
    }
}
