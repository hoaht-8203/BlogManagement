using blog_server.DTOs.User;
using blog_server.Models;
using blog_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize(Roles = "ADMIN")]
        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<List<ListUserResponse>>>> List(
            [FromQuery] ListUserRequest request
        )
        {
            var response = await _userService.ListUsers(request);
            return Ok(
                ApiResponse<List<ListUserResponse>>.SuccessResponse(
                    response,
                    "Get list user success"
                )
            );
        }
    }
}
