using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SkyLinkApi.Models;

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

public partial class ApiResponse<T> where T : class
{
    [property: DataMember][JsonPropertyName("items")] public IEnumerable<T> Items { get; set; } = [];

    [property: DataMember][JsonPropertyName("total_items")] public int TotalItems { get; set; }
}