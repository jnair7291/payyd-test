using payyd_test.application.Interfaces;
using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<ResultOrError<string, ErrorResponse>> CreateCustomerOnPG() {
            return null;
        }
        public Task<ResultOrError<string, ErrorResponse>> GetAllCustomers() {
            return null;
        }
        public Task<ResultOrError<string, ErrorResponse>> GetCustomerById() {
            return null;}
    }
}
