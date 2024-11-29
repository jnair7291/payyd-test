using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpPost(Name = "GetPaymentUpdate")]
        public IActionResult GetPaymentUpdate()
        {
            return null; ;
        }
    }
}
