using System.Threading.Tasks;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIWDapper.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result =  await _employeeService.GetEmployeeList();

        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    /// <summary>Uset this to get employee by id</summary>
    public async Task<IActionResult> GetEmployee(int id)
    {
        var result =  await _employeeService.GetEmployee(id);

        return Ok(result);
    }
    
    [HttpPost]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> AddEmployee([FromBody]Employee employee)
    {
        var result =  await _employeeService.CreateEmployee(employee);

        return Ok(result);
    }
    
    [HttpPut]
    /// <summary>Uset this to update employees</summary>
    public async Task<IActionResult> UpdateEmployee([FromBody]Employee employee)
    {
        var result =  await _employeeService.UpdateEmployee(employee);

        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    /// <summary>Uset this to delete an employee</summary>
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result =  await _employeeService.DeleteEmployee(id);

        return Ok(result);
    }
}