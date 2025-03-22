using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Category;

public class DeleteCategoryRequest
{
    [Required]
    public int CategoryId { get; set; }
}
