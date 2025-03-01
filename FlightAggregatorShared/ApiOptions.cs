namespace FlightAggregatorShared.Models;


public class ApiOptions
{
    public string? DepartureAirportCode { get; set; }
    public string? DestinationAirportCode { get; set; }
    public string? Airline { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public DateTime? DepartureDate { get; set; }
    public int? MaxLayovers { get; set; }
    public string? SortLabel { get; set; }
    public int SortDirection { get; set; } = 1;
}

public partial class ApiResponse<T> where T : class
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalItems { get; set; }
}
