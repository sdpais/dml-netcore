using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DML.Domain.Entities;
using DML.Services;

namespace WebAPIWDapper.Controllers;
[ApiController]
[Route("[controller]")]
public class ContactUsController : Controller
{
    private readonly IConfiguration _config;
    private ContactUsService _contactUsService;
    //private readonly IRedisCacheHandler _redisCacheHandler;

    public ContactUsController(IConfiguration config)
    {
        _config = config;
        _contactUsService = new ContactUsService(_config);
        //  _redisCacheHandler = new RedisCacheHandler(config);
    }
    [HttpPost]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> AddContactUs([FromBody] ContactUs contactUs)
    {
        var result = await _contactUsService.CreateContactUs(contactUs);

        return Ok(result);
    }
    [HttpPut]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> UpdateContactUs([FromBody] ContactUs contactUs)
    {
       
        var result = await _contactUsService.UpdateContactUs(contactUs);

        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactUs(string id)
    {
        var result = await _contactUsService.DeleteContactUs(id);

        return Ok(result);
    }
    [HttpGet]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> GetContactUs()
    {
        var result = await _contactUsService.GetContactUs();

        return Ok(result);
    }
    [HttpGet("{id}")]
    /// <summary>Uset this to add employees</summary>
    public async Task<IActionResult> GetContactUs(string id)
    {
        var result = await _contactUsService.GetContactUs(id);

        return Ok(result);
    }
}
