using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        [HttpPost(Name = "CreateTemporaryCardForPurchase")]
        public IActionResult CreateTemporaryCardForPurchase()
        {
            return null;
        }

        [HttpPost(Name = "CreateCardOnFile")]
        public IActionResult CreateCardOnFile()
        {
            return null;
        }

        [HttpPost(Name = "AddCreditstoCard")]
        public IActionResult AddCreditstoCard()
        {
            return null;
        }
    }
}
