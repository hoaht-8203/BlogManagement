using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Category;

public class DetailCategoryRequest
{
    [Required]
    public int CategoryId { get; set; }
}
