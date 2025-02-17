using FlightAggregator.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace FlightAggregator.Controllers;


[ApiController]
[Route("api")]
public class SearchController(ISearchService searchService)
{
    [HttpPost("search")]
    public async Task Search()
    {

    }
}