using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet(Name = "GetCustomerDtails")]
        public Task<IActionResult> GetMyCustomers()
        {
            return null;
        }

        [HttpPost(Name = "CreateCustomer")]
        public Task<IActionResult> CreateCustomerOnPG()
        {
            return null;
        }

        [HttpGet(Name = "GetCustomerDtailsById")]
        public Task<IActionResult> GetCustomerDetailsById()
        {
            return null;
        }


    }
}
