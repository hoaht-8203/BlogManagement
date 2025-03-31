using blog_server.Constants;
using blog_server.CustomAttributes;
using blog_server.DTOs.Category;
using blog_server.Models;
using blog_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [Transaction]
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<CreateCategoryResponse>>> Create(
            [FromBody] CreateCategoryRequest request
        )
        {
            var response = await _categoryService.CreateCategory(request);
            return Ok(
                ApiResponse<CreateCategoryResponse>.SuccessResponse(
                    response,
                    "Create new category success"
                )
            );
        }

        [HttpGet("detail")]
        public async Task<ActionResult<ApiResponse<DetailCategoryResponse>>> Detail(
            [FromQuery] DetailCategoryRequest request
        )
        {
            var response = await _categoryService.DetailCategory(request);
            return Ok(
                ApiResponse<DetailCategoryResponse>.SuccessResponse(
                    response,
                    "Get detail category success"
                )
            );
        }

        [Transaction]
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<UpdateCategoryResponse>>> Update(
            [FromBody] UpdateCategoryRequest request
        )
        {
            var response = await _categoryService.UpdateCategory(request);
            return Ok(
                ApiResponse<UpdateCategoryResponse>.SuccessResponse(
                    response,
                    "Update category success"
                )
            );
        }

        [Transaction]
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<DeleteCategoryResponse>>> Delete(
            [FromBody] DeleteCategoryRequest request
        )
        {
            var response = await _categoryService.DeleteCategory(request);
            return Ok(
                ApiResponse<DeleteCategoryResponse>.SuccessResponse(
                    response,
                    "Delete category success"
                )
            );
        }

        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<ListCategoryResponse>>> List(
            [FromQuery] ListCategoryRequest request
        )
        {
            var response = await _categoryService.ListCategories(request);
            return Ok(
                ApiResponse<ListCategoryResponse>.SuccessResponse(
                    response,
                    "Get list categories success"
                )
            );
        }
    }
}
