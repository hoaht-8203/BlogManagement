using blog_server.Constants;

namespace blog_server.DTOs.Category;

public class ChildCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public AppStatus Status { get; set; }
}
