using Microsoft.AspNetCore.Mvc;
using NimbusApi.Abstracts;
using NimbusApi.Entity;
using NimbusApi.Models;

namespace NimbusApi.Controllers;


[ApiController]
[Route("api/flights")]
public class SearchController(ISearchService searchService)
{
    [HttpGet("search")]
    public async Task<List<FlightEntity>> Search([FromQuery] ApiOptions options,
        CancellationToken cancellationToken)
    {
        return await searchService.SearchFlights(options, cancellationToken);
    }
}
