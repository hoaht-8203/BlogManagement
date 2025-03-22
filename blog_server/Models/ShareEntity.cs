using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_server.Models;

public class BaseEntity
{
    [Column("create_date")]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    [Column("update_date")]
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

    [Column("create_by")]
    public Guid? CreateBy { get; set; }

    [Column("update_by")]
    public Guid? UpdateBy { get; set; }
}
