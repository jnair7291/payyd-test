using Microsoft.AspNetCore.Mvc;
using payyd_test.application.Interfaces;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<UserController> _logger;
        [HttpPost(Name = "MakePaymentForCustomer")]
        public async Task<IActionResult> MakePaymentForCustomer(string customerid)
        {
            try
            {
                if (customerid == null)
                {
                    return BadRequest("Error: Person ID cannot be zero.");
                }
                if (!HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth) || string.IsNullOrWhiteSpace(headerAuth.FirstOrDefault()))
                {
                    return StatusCode(401, "Error: Authorization header is missing.");
                }
                var token = headerAuth.FirstOrDefault()?.Split(' ').LastOrDefault();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return StatusCode(401, "Error: Authorization token is missing or invalid.");
                }
                var result = await _transactionService.MakePaymentAsync(customerid);

                if (result.IsError)
                {
                    // Handle the case where the result is null (unexpected error).
                    _logger.LogError("Payment failed ", customerid);
                    return StatusCode(500, result);
                }

                // Handle successful response.
                else
                {
                    _logger.LogError("Payment Sucessful", result);
                    return StatusCode(200, "Payment Sucessful");
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected server error occurred.");
            }
        }
    }
    
}
