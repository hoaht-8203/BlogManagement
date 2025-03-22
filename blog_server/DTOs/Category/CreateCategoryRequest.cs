using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Category;

public class CreateCategoryRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public int? ParentId { get; set; }
}
