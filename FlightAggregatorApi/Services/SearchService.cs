using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Services;


public class SearchService() : ISearchService
{
    public Task<List<FlightView>> SearchFlights(ApiOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
