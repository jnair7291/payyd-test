using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThirdPartyController : ControllerBase
    {
        [HttpGet(Name = "GetConfigsDetails")]
        public IActionResult Index()
        {
            return null;
        }
    }
}
