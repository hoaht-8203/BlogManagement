using blog_server.DTOs.Type;
using blog_server.Models;
using blog_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController(ITypeService typeService) : ControllerBase
    {
        private readonly ITypeService _typeService = typeService;

        [Authorize(Roles = "ADMIN")]
        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<List<object>>>> List(
            [FromQuery] ListTypeRequest request
        )
        {
            var response = await _typeService.ListType(request);
            return Ok(ApiResponse<List<object>>.SuccessResponse(response, "Get list type success"));
        }
    }
}
