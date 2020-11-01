using System.Threading.Tasks;
using ImprovTime.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImprovQuery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImprovController : ControllerBase
    {
     
        private readonly ILogger<ImprovController> _logger;

        public ImprovController(ILogger<ImprovController> logger)
        {
            _logger = logger;
            
        }

        [HttpPost("query")]
        public async Task<QueryResult> Query([FromBody] Query q)
        {
            var query = new QueryGrain();
            var result = await query.Query(q);
            return result;
        }
    }
}