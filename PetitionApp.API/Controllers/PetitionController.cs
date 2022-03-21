using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetitionApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetitionController :ControllerBase
    {

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Success");
        }
    }
}
