using System.ComponentModel.DataAnnotations;

namespace SkyLinkApi.Entity;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
