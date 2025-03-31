using blog_server.Constants;

namespace blog_server.DTOs.Category;

public class ListCategoryResponse
{
    public List<ParentCategoryDto> Categories { get; set; } = [];
}

public class ParentCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ChildCategoryDto> Children { get; set; } = [];
}
