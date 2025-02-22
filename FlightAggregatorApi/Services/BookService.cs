using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Services;

public class BookService() : IBookService
{
    public Task<long> Create(string userId, long FlightId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<BookView>> GetAll(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
