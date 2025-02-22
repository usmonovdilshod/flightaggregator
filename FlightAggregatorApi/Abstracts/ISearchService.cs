
using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Abstracts;

public interface ISearchService
{ 
    public Task<List<FlightView>> SearchFlights(ApiOptions options, CancellationToken cancellationToken);
}
