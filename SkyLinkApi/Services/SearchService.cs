using SkyLinkApi.Abstracts;
using SkyLinkApi.Entity;
using SkyLinkApi.Models;

namespace SkyLinkApi.Services;

public class SearchService : ISearchService
{
    public Task<List<FlightEntity>> SearchFlights(ApiOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
