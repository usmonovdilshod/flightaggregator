using FlightAggregatorApi.Abstracts;
using FlightAggregatorShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAggregatorApi.Controllers;


[ApiController]
[Route("api/flights")]
public class SearchController(ISearchService searchService)
{
    [HttpGet("search")]
    public async Task<ApiResponse<FlightResponse>> Search([FromQuery] ApiOptions options,
        CancellationToken cancellationToken)
    {
        return await searchService.SearchFlights(options, cancellationToken);
    }
}
