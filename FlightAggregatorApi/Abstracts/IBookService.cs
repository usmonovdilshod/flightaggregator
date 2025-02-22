using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Abstracts;

public interface IBookService
{ 
    public Task<List<BookView>> GetAll(string userId, CancellationToken cancellationToken);
    public Task<long> Create(string userId, long FlightId, CancellationToken cancellationToken);
}
