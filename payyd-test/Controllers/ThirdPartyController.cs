using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThirdPartyController : ControllerBase
    {
        [HttpPost(Name = "GetMerchantDetails")]
        public IActionResult Index()
        {
            return null;
        }
    }
}
