using Microsoft.AspNetCore.Mvc;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet(Name = "GetTransactionsDetails")]
        public IActionResult Index()
        {
            return null;
        }
    }
}
