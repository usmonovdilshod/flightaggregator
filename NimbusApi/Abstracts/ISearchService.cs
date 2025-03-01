using NimbusApi.Entity;
using NimbusApi.Models;

namespace NimbusApi.Abstracts;

public interface ISearchService
{ 
    public Task<List<FlightEntity>> SearchFlights(ApiOptions options, CancellationToken cancellationToken);
}
