using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using blog_server.Constants;

namespace blog_server.Models;

[Table("roles")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public AppRole Name { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = [];
}
