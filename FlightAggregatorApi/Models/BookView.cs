namespace FlightAggregatorApi.Models;

public class BookView
{
    public string UserId { get; set; } = null!;
    public long FlightId { get; set; }
    public virtual FlightView Flight { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
}
public class BookResponse
{
    public string UserId { get; set; } = null!;
    public long FlightId { get; set; }
    public virtual FlightView Flight { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public string Source { get; set; } = null!;
}
