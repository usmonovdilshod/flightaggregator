using System.ComponentModel.DataAnnotations;

namespace NimbusApi.Entity;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
