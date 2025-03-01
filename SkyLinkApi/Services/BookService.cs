using Microsoft.EntityFrameworkCore;
using SkyLinkApi.Abstracts;
using SkyLinkApi.Data;
using SkyLinkApi.Entity;

namespace SkyLinkApi.Services;

public class BookService(AppDbContext context) : IBookService
{
    public async Task<long> Create(string userId, long flightId, CancellationToken cancellationToken)
    {

        var booking = new BookEntity
        {
            UserId = userId,
            FlightId = flightId,
            CreatedAt = DateTime.UtcNow
        };

        await context.AddAsync(booking, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return booking.Id; 
    }

    
    public async Task<List<BookEntity>> GetAll(string userId, CancellationToken cancellationToken)
    {

        return await context.Books
            .Include(b => b.Flight) 
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt) 
            .ToListAsync(cancellationToken);
    }
}
