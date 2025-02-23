namespace NimbusApi.Models;


public class ApiOptions
{
    public string? DepartureAirportCode { get; set; }
    public string? DestinationAirportCode { get; set; }
    public string? Airline { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public DateTime? DepartureDate { get; set; }
    public int? MaxLayovers { get; set; }
}

