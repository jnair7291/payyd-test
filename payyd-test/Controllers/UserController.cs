using Microsoft.AspNetCore.Mvc;
using payyd_test.application.Interfaces;
using System.Net;

namespace payyd_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "text/json")]
    public class UserController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<UserController> _logger;

       

        [HttpPost("/api/CreateCustomerIfNotExists")]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateCustomerOnPG([FromBody] int personId)
        {
            try
            {
                if (personId == 0)
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
                var result = await _customerService.CreateCustomerOnPG(personId);

                if (result == null)
                {
                    // Handle the case where the result is null (unexpected error).
                    _logger.LogError("CreateCustomerOnPG returned a null result for personId: {PersonId}", personId);
                    return StatusCode(500, "An unexpected error occurred.");
                }

                // Handle successful response.
                else {
                    _logger.LogError("Customer created", result);
                    return StatusCode(200, "Customer created successfully on payment gateway");
                }

             

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected server error occurred.");
            }
        }

        [HttpGet(Name = "GetCustomerDtails")]
        public Task<IActionResult> GetMyCustomers()
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
