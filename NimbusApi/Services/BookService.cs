using NimbusApi.Abstracts;
using NimbusApi.Entity;

namespace NimbusApi.Services;

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
