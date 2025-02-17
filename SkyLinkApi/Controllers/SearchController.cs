using Microsoft.AspNetCore.Mvc;
using SkyLinkApi.Abstracts;

namespace SkyLinkApi.Controllers;


[ApiController]
[Route("api")]
public class SearchController(ISearchService searchService)
{
    [HttpGet("search")]
    public async Task Search()
    {

    }
}
