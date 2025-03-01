namespace FlightAggregatorShared.Models;

public class FlightResponse
{
    public long Id { get; set; }
    public string Airline { get; set; } = null!;
    public double Price { get; set; }
    public string DepartureAirportCode { get; set; } = null!;
    public string DestinationAirportCode { get; set; } = null!;
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int Layovers { get; set; }
    public string Source { get; set; } = null!;
}
