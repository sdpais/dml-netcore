using System.Threading.Tasks;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPIWDapper.BusinessLogic;

namespace WebAPIWDapper.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly IConfiguration _config;
    private readonly IRedisCacheHandler _redisCacheHandler;

    public EmployeesController(IConfiguration config)
    {
        _config = config;
        _redisCacheHandler = new RedisCacheHandler(config);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = false; 
        //var result = await _employeeService.GetEmployeeList();

        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    /// <summary>Uset this to get employee by id</summary>
    public async Task<IActionResult> GetEmployee(int id)
    {
        var result = false; // await _employeeService.GetEmployee(id);

        return Ok(result);
    }
    
    [HttpPost]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> AddEmployee([FromBody]Employee employee)
    {
        EmployeeBLService employeeBLService = new EmployeeBLService(_config);
        var result =  await employeeBLService.CreateEmployee(employee);

        return Ok(result);
    }
    
    [HttpPut]
    /// <summary>Uset this to update employees</summary>
    public async Task<IActionResult> UpdateEmployee([FromBody]Employee employee)
    {
        EmployeeBLService employeeBLService = new EmployeeBLService(_config);
        var result = await employeeBLService.UpdateEmployee(employee);

        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    /// <summary>Uset this to delete an employee</summary>
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = false; // await _employeeService.DeleteEmployee(id);

        return Ok(result);
    }
}