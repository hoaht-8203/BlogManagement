namespace blog_server.Models;

public class ApiResponse<T>(
    bool success = true,
    string message = "",
    T? data = default,
    List<string>? errors = null
)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
    public List<string>? Errors { get; set; } = errors;

    public static ApiResponse<T> SuccessResponse(
        T data,
        string message = "Operation completed successfully"
    )
    {
        return new ApiResponse<T>(true, message, data);
    }

    public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>(false, message, default, errors);
    }
}
