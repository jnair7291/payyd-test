using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        [HttpGet(Name = "GetWallteruserDetails")]
        public IActionResult Index()
        {
            return null;
        }
    }
}
