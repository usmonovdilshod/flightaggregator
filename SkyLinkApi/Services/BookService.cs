using SkyLinkApi.Abstracts;
using SkyLinkApi.Entity;

namespace SkyLinkApi.Services;

public class BookService : IBookService
{
    public Task<long> Create(string userId, long FlightId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<BookEntity>> GetAll(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
