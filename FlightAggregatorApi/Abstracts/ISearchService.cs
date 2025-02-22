
using FlightAggregatorApi.Models;

namespace FlightAggregatorApi.Abstracts;

public interface ISearchService
{ 
    public Task<ApiResponse<FlightResponse>> SearchFlights(ApiOptions options, CancellationToken cancellationToken);
}
