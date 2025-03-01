namespace FlightAggregatorShared.Models;

public class BookView
{
    public string UserId { get; set; } = null!;
    public long FlightId { get; set; }
    public virtual FlightView Flight { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
}
