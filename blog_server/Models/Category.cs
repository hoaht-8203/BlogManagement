using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using blog_server.Constants;

namespace blog_server.Models;

[Table("categories")]
public class Category : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [JsonIgnore]
    public Category? Parent { get; set; }

    [JsonIgnore]
    public ICollection<Category> Children { get; set; } = [];

    [Column("status")]
    public AppStatus Status { get; set; } = AppStatus.Active;
}
