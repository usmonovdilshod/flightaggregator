
using SkyLinkApi.Entity;
using SkyLinkApi.Models;

namespace SkyLinkApi.Abstracts;

public interface ISearchService
{
    public Task<List<FlightEntity>> SearchFlights(ApiOptions options, CancellationToken cancellationToken);
}
