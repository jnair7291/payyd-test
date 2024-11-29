using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using payyd_test.application.Interfaces;
using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Square;
using Square.Authentication;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Square.Models;

namespace payyd_test.infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PayydDbContext _dBContext;
        private static ISquareClient squareClient;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(IConfiguration configuration, PayydDbContext dBContext, ILogger<CustomerRepository> logger) {
            _configuration = configuration;
            _dBContext = dBContext;
            _logger = logger;
            var accessToken = $"{_configuration.GetSection("Square:AccessToken").Value}";
            var environment = $"{_configuration.GetSection("Square:Environment").Value}";
            squareClient = new SquareClient.Builder().Environment(environment == "Production" ? Square.Environment.Production : Square.Environment.Sandbox)
                   .BearerAuthCredentials(new BearerAuthModel.Builder(accessToken).Build())
               .Build();
        }
        public async Task<ResultOrError<string, ErrorResponse>> CreateCustomerOnPG(int userId) {
            try
            {

                _logger.Log(LogLevel.Information, "Creating user on PG");
                string cutstomerId = string.Empty;
                var squareIdentity = await _dBContext.UserModel.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (squareIdentity?.PGCustomerId != null)
                    return new ResultOrError<string, ErrorResponse>(squareIdentity.PGCustomerId!);
                else
                {

                    var emailAddress = new CustomerTextFilter.Builder()
                    .Exact(squareIdentity?.Useremailid)
                    .Build();

                    var filter = new CustomerFilter.Builder()
                      .EmailAddress(emailAddress)
                      .Build();

                    var query = new CustomerQuery.Builder()
                      .Filter(filter)
                      .Build();

                    var body = new SearchCustomersRequest.Builder()
                      .Query(query)
                      .Count(true)
                      .Build();


                    var searchCustomerResult = await squareClient.CustomersApi.SearchCustomersAsync(body: body);
                    if (searchCustomerResult.Customers != null && searchCustomerResult.Customers.Any())
                    {
                        cutstomerId = searchCustomerResult.Customers[0].Id;
                    }
                    else
                    {
                        var createCustomerRequest = new CreateCustomerRequest.Builder().IdempotencyKey(Guid.NewGuid().ToString())
                                                                  .GivenName($"{squareIdentity?.UserName}")
                                                                  .CompanyName("payyd")
                                                                  .EmailAddress($"{squareIdentity?.Useremailid}")// send proper email id
                                                                  .ReferenceId(Convert.ToString(squareIdentity.UserId))
                                                                  .Note("Square Customer for Payyd")
                                                                  .Build();

                        var createCustomerResponse = squareClient.CustomersApi.CreateCustomer(createCustomerRequest);
                        cutstomerId = createCustomerResponse.Customer.Id;


                        squareIdentity.PGCustomerId = cutstomerId;

                        _dBContext.UserModel.Update(squareIdentity);
                        await _dBContext.SaveChangesAsync();

                    }
                    return new ResultOrError<string, ErrorResponse>(cutstomerId!);
                }
            }
            catch (Exception e) {
                _logger.Log(LogLevel.Error,Convert.ToString(Errors.SomethingWentWrong));
                var error = new ErrorResponse(Errors.SomethingWentWrong, e.Message);
                return new ResultOrError<string, ErrorResponse>(error);
            }
        }
        public Task<ResultOrError<string, ErrorResponse>> GetAllCustomers() {
            return null;
        }
        public Task<ResultOrError<string, ErrorResponse>> GetCustomerById() {
            return null;}
    }
}
