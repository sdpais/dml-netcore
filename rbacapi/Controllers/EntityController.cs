using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

using rbacapi.Model;

namespace rbacapi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EntityController : ControllerBase
    {
        [HttpGet(Name = "entities")]
        [MapToApiVersion("1.0")]
        public IEnumerable<Entity> Get()
        {
            var result = new List<Entity>();

            return result;
        }

    }
}
