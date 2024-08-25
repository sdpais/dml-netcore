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
        [HttpPost(Name = "entities")]
        [MapToApiVersion("1.0")]
        public Entity Post([FromBody] Entity entity)
        {
            var result = new List<Entity>();

            return result;
        }
        [HttpPut(Name = "entities")]
        [MapToApiVersion("1.0")]
        public IEnumerable<Entity> Put()
        {
            var result = new List<Entity>();

            return result;
        }
        [HttpDelete(Name = "entities")]
        [MapToApiVersion("1.0")]
        public IEnumerable<Entity> Delete()
        {
            var result = new List<Entity>();

            return result;
        }
    }

}
