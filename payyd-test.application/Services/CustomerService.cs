using payyd_test.application.Interfaces;
using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.application.Services
{
    public class CustomerService :ICustomerService
    {
        public ICustomerRepository _customerRespository;

        public CustomerService(ICustomerRepository customerRespository)
        {
            _customerRespository = customerRespository;
        }

        public async Task<ResultOrError<string, ErrorResponse>> CreateCustomerOnPG(int personId) {
            return await _customerRespository.CreateCustomerOnPG(personId);
        }
        public async Task<ResultOrError<string, ErrorResponse>> GetAllCustomers() {
                return await _customerRespository.GetAllCustomers();
        }
        public async Task<ResultOrError<string, ErrorResponse>> GetCustomerById() {
            // Fetch customer by ID from the repository
                return await _customerRespository.GetCustomerById();
           
        }
    }
}
