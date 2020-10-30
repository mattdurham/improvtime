using System.Threading.Tasks;
using ImprovTime.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;

namespace ImprovQuery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImprovController : ControllerBase
    {
     
        private readonly ILogger<ImprovController> _logger;
        private IClusterClient _client;

        public ImprovController(ILogger<ImprovController> logger, IClusterClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpPost("query")]
        public async Task<QueryResult> Query([FromBody] Query q)
        {
            var grain = _client.GetGrain<IQueryGrain>(0);
            var result = await grain.Query(q);
            return result;
        }
    }
}