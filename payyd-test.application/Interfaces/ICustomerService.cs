using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.application.Interfaces
{
    public interface ICustomerService
    {
        public Task<ResultOrError<string, ErrorResponse>> CreateCustomerOnPG();
        public Task<ResultOrError<string, ErrorResponse>> GetAllCustomers();
        public Task<ResultOrError<string, ErrorResponse>> GetCustomerById();
    }
}
