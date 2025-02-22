using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FlightAggregatorApi.Models;


public class ApiOptions
{
    public string? DepartureAirportCode { get; set; }
    public string? DestinationAirportCode { get; set; }
    public string? Airline { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public int? MaxLayovers { get; set; }
}

public class BookView
{
    
}
public class FlightView
{
    public long Id { get; set; }
    public string Airline { get; set; } = null!;
    public double Price { get; set; }
    public string DepartureAirportCode { get; set; } = null!;
    public string DestinationAirportCode { get; set; } = null!;
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int Layovers { get; set; }
}

public partial class ApiResponse<T> where T : class
{
    [property: DataMember][JsonPropertyName("items")] public IEnumerable<T> Items { get; set; } = [];

    [property: DataMember][JsonPropertyName("total_items")] public int TotalItems { get; set; }
}
