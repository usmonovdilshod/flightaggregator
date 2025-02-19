using Microsoft.AspNetCore.Mvc;
using NimbusApi.Abstracts;
using NimbusApi.Entity;
using NimbusApi.Models;

namespace NimbusApi.Controllers;

[ApiController]
[Route("api")]
public class BookController(IBookService service)
{
    [HttpGet("books")]
    public async Task<List<BookEntity>> GetAll([FromQuery] string userId, CancellationToken cancellationToken)
    {
        return await service.GetAll(userId, cancellationToken);
    }
    
    [HttpPost("books")]
    public async Task<long> Create([FromBody] BookCreateRequest data, CancellationToken cancellationToken)
    {
        return await service.Create(data.UserId, data.FlightId, cancellationToken);
    }
}
