using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAggregatorApi.Controllers;

[ApiController]
[Route("api")]
public class BookController(IBookService service)
{
    [HttpGet("books")]
    public async Task<List<BookResponse>> GetAll([FromQuery] string userId, CancellationToken cancellationToken)
    {
        return await service.GetAll(userId, cancellationToken);
    }
    
    [HttpPost("books")]
    public async Task<long> Create([FromBody] BookCreateRequest data, CancellationToken cancellationToken)
    {
        return await service.Create(data.UserId, data.FlightId, data.Source, cancellationToken);
    }
}
