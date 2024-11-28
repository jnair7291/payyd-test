using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost(Name = "MakePaymentForCustomer")]
        public IActionResult MakePaymentForCustomer(int custoemrId)
        {
            return null;
        }
    }
}
