
using SkyLinkApi.Entity;

namespace SkyLinkApi.Abstracts;

public interface IBookService
{ 
    public Task<List<BookEntity>> GetAll(string userId, CancellationToken cancellationToken);
    public Task<long> Create(string userId, long FlightId, CancellationToken cancellationToken);
}
