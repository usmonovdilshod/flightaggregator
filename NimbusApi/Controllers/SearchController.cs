using Microsoft.AspNetCore.Mvc;
using NimbusApi.Abstracts;

namespace NimbusApi.Controllers;


[ApiController]
[Route("api")]
public class SearchController(ISearchService searchService)
{
    [HttpGet("search")]
    public async Task Search()
    {

    }
}
