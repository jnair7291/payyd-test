using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet(Name = "GetUserDetails")]
        public Task<IActionResult> Index()
        {
            return null;
        }
    }
}
