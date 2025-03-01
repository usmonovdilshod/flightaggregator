
namespace NimbusApi.Entity;

public class BookEntity : BaseEntity
{
    public string UserId { get; set; } = null!;
    public long FlightId { get; set; }
    public virtual FlightEntity Flight { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
