using blog_server.CustomAttributes;
using blog_server.DTOs.Role;
using blog_server.Models;
using blog_server.Services;
using blog_server.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<PaginatedList<ListRoleResponse>>>> List(
            [FromQuery] ListRoleRequest request
        )
        {
            var response = await _roleService.ListRoles(request);
            return Ok(
                ApiResponse<PaginatedList<ListRoleResponse>>.SuccessResponse(
                    response,
                    "Get list role success"
                )
            );
        }

        [Transaction]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateRoleRequest request)
        {
            await _roleService.CreateRole(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Create new role success"));
        }

        [Transaction]
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] UpdateRoleRequest request)
        {
            await _roleService.UpdateRole(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Update role success"));
        }

        [Transaction]
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteRoleRequest request)
        {
            await _roleService.DeleteRole(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Delete role success"));
        }
    }
}
