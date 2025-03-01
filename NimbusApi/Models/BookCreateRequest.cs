using System.ComponentModel.DataAnnotations;

namespace NimbusApi.Models;


public class BookCreateRequest
{
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "FlightId is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "FlightId must be greater than 0.")]
    public long FlightId { get; set; }
}
