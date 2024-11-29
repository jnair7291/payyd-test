using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "text/json")]
    public class ThirdPartyController : ControllerBase
    {
        [HttpPost(Name = "GetMerchantDetails")]
        public IActionResult Index()
        {
            return null;
        }
    }
}
