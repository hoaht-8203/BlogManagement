using System.ComponentModel.DataAnnotations.Schema;

namespace blog_server.Models;

public class BaseEntity
{
    [Column("create_date")]
    public DateTime CreateDate { get; set; }

    [Column("update_date")]
    public DateTime UpdateDate { get; set; }

    [Column("create_by")]
    public string? CreateBy { get; set; }

    [Column("update_by")]
    public string? UpdateBy { get; set; }
}
