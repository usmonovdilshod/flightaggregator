
using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Abstracts;

public interface ISearchService
{ 
    public Task<List<FlightResponse>> SearchFlights(ApiOptions options, CancellationToken cancellationToken);
}
