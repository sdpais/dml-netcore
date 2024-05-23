using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPIWDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExceptionController : ControllerBase
    {
        //[HttpGet("not-found")]
        //public ActionResult GetNotFound()
        //{
        //    // Simulate a situation where a resource is not found
        //    throw new NotFoundException("The requested resource was not found.");
        //}

        [HttpGet("invalid")]
        public ActionResult GetInvalid()
        {
            // Simulate a validation error
            throw new ValidationException("Validation failed for the request.");
        }

        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            // Simulate unauthorized access
            throw new UnauthorizedAccessException("You do not have permission to access this resource.");
        }

        
    }
}
