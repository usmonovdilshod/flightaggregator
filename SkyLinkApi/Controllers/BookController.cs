using Microsoft.AspNetCore.Mvc;
using SkyLinkApi.Abstracts;
using SkyLinkApi.Entity;
using SkyLinkApi.Models;

namespace SkyLinkApi.Controllers;

[ApiController]
[Route("api")]
public class BookController(IBookService service)
{
    [HttpGet("books")]
    public async Task<List<BookEntity>> GetAll([FromQuery] string userId, CancellationToken cancellationToken)
    {
        try
        {
            return await service.GetAll(userId, cancellationToken);
        }
        catch (Exception)
        {
            return new List<BookEntity>();
        }
    }
    
    [HttpPost("books")]
    public async Task<long> Create([FromBody] BookCreateRequest data, CancellationToken cancellationToken)
    {
        return await service.Create(data.UserId, data.FlightId, cancellationToken);
    }
}
