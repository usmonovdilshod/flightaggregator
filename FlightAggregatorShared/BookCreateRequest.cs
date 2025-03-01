using System.ComponentModel.DataAnnotations;

namespace FlightAggregatorShared.Models;


public class BookCreateRequest
{
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; } = null!;
    [Required(ErrorMessage = "Source is required.")]
    public string Source { get; set; } = null!;

    [Required(ErrorMessage = "FlightId is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "FlightId must be greater than 0.")]
    public long FlightId { get; set; }
}

public class BookCreateExternalRequest
{
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "FlightId is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "FlightId must be greater than 0.")]
    public long FlightId { get; set; }
}
