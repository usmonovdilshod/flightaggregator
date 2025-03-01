using FlightAggregatorApi.Abstracts;
using FlightAggregatorShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAggregatorApi.Controllers;

[ApiController]
[Route("api")]
public class BookController(IBookService service)
{
    [HttpGet("books")]
    public async Task<ApiResponse<BookResponse>> GetAll([FromQuery] string userId, [FromQuery] ApiOptions options, CancellationToken cancellationToken)
    {
        try
        {
            return await service.GetAll(userId, options, cancellationToken);
        }
        catch (Exception)
        {
            return new ApiResponse<BookResponse>();
        }
    }

    [HttpPost("books")]
    public async Task<long> Create([FromBody] BookCreateRequest data, CancellationToken cancellationToken)
    {
        return await service.Create(data.UserId, data.FlightId, data.Source, cancellationToken);
    }
}
