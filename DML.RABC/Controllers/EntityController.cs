using Microsoft.AspNetCore.Mvc;

using DML.RBAC.Domain;
using DML.RBAC.Service;
using Serilog;

namespace DML.RABC.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EntityController : ControllerBase
{
    private readonly IConfiguration _config;
    private EntityService _entityService;

    public EntityController(IConfiguration config)
    {
        _config = config;
        _entityService = new EntityService(_config);
    }


    [HttpGet(Name = "entities")]
    public async Task<IActionResult> Get()
    {
        Log.Information("what am I getting?");
        var result = await _entityService.Get();
        
        return Ok(result);
    }
    [HttpPost(Name = "entities")]
    public async Task<IActionResult> Post([FromBody] Entity entity)
    {
        var result = _entityService.Create(entity);
        return Ok(result); 
    }
    [HttpPut(Name = "entities")]
    
    public IEnumerable<Entity> Put()
    {
        var result = new List<Entity>();

        return result;
    }
    [HttpDelete(Name = "entities")]
    
    public IEnumerable<Entity> Delete()
    {
        var result = new List<Entity>();

        return result;
    }
}
