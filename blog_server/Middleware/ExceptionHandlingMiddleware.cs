using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using blog_server.Exceptions;
using blog_server.Models;

namespace blog_server.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ApiResponse<object> { Success = false };

            switch (error)
            {
                case ValidationException validationEx:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    errorResponse.Message = "Validation failed";
                    errorResponse.Errors = [validationEx.Message];
                    break;
                case ApiException apiEx:
                    response.StatusCode = apiEx.StatusCode;
                    errorResponse.Message = apiEx.Message;
                    errorResponse.Errors = apiEx.Errors;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    errorResponse.Message = "The requested resource was not found.";
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    errorResponse.Message = "An internal server error occurred.";
                    errorResponse.Errors = [error.Message];
                    break;
            }

            _logger.LogError(error, error.Message);

            var result = JsonSerializer.Serialize(errorResponse, _jsonOptions);
            await response.WriteAsync(result);
        }
    }
}
