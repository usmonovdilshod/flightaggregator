using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Abstracts;

public interface IBookService
{ 
    public Task<List<BookResponse>> GetAll(string userId, CancellationToken cancellationToken);
    public Task<long> Create(string userId, long FlightId, string source, CancellationToken cancellationToken);
}
