using Microsoft.AspNetCore.Mvc;

namespace FlightAggregatorApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class SearchController : ControllerBase
    {
        [HttpGet("search")]
        public async Task<List<string>> Search(
         CancellationToken cancellationToken)
        {
            return ["sadas", "dsadasd"];
        }
    }
}
