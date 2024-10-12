using Microsoft.AspNetCore.Mvc;

using DML.Domain.Entities;
using prommetrics.Service;
using static Dapper.SqlMapper;
//using Serilog;

namespace prommetrics.Controllers;

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
        //Log.Information("what am I getting?");
        var result = await _entityService.Get();
        
        return Ok(result);
    }
    [HttpPost(Name = "entities")]
    public async Task<IActionResult> Post([FromBody] Entity entity)
    {
        var result = await _entityService.Create(entity);
        return Ok(result); 
    }
    [HttpPut(Name = "entities")]
    
    public async Task<IActionResult> Put([FromBody] Entity entity)
    {
        var result = await _entityService.Update(entity);

        return Ok(result); 
    }
    [HttpDelete(Name = "entities")]
    
    public async Task<IActionResult> Delete([FromBody] Entity entity)
    {
        var result = await _entityService.Delete(entity);

        return Ok(result); 
    }
}
