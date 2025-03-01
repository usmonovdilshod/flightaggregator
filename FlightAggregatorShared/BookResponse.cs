namespace FlightAggregatorShared.Models;

public class BookResponse
{
    public string UserId { get; set; } = null!;
    public long FlightId { get; set; }
    public virtual FlightView Flight { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public string Source { get; set; } = null!;
}
