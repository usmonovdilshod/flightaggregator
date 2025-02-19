using NimbusApi.Abstracts;
using NimbusApi.Entity;
using NimbusApi.Models;

namespace NimbusApi.Services;

public class SearchService : ISearchService
{
    public Task<List<FlightEntity>> SearchFlights(ApiOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
