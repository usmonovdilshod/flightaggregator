using Microsoft.AspNetCore.Mvc;
using SkyLinkApi.Abstracts;
using SkyLinkApi.Entity;
using SkyLinkApi.Models;

namespace SkyLinkApi.Controllers;


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
