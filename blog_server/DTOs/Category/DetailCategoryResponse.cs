using System;

namespace blog_server.DTOs.Category;

public class DetailCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Models.Category> Children { get; set; } = [];
}
