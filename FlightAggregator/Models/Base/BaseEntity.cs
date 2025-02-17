using System.ComponentModel.DataAnnotations;

namespace FlightAggregator.Models;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
