using blog_server.DTOs.Category;

namespace blog_server.Services;

public interface ICategoryService
{
    public Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request);
    public Task<DetailCategoryResponse> DetailCategory(DetailCategoryRequest request);
    public Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest request);
    public Task<DeleteCategoryResponse> DeleteCategory(DeleteCategoryRequest request);
    public Task<ListCategoryResponse> ListCategories(ListCategoryRequest request);
}
