using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Category;

public class UpdateCategoryRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public int? ParentId { get; set; }
}
