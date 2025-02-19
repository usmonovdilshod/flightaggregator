using System.Text.Json.Serialization;

namespace SkyLinkApi.Models;

public class BookCreateRequest
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
    [JsonPropertyName("flight_id")] public long FlightId { get; set; }
}
