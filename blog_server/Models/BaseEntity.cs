using System.ComponentModel.DataAnnotations.Schema;

namespace blog_server.Models;

public abstract class BaseEntity
{
    [Column("create_date")]
    public DateTime CreateDate { get; set; }

    [Column("update_date")]
    public DateTime UpdateDate { get; set; }

    [Column("create_by")]
    public Guid? CreateBy { get; set; }

    [Column("update_by")]
    public Guid? UpdateBy { get; set; }
}
