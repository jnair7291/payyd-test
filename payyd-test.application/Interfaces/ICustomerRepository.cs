using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using payyd_test.domain.Common;

namespace payyd_test.application.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<ResultOrError<string, string>> CreateCustomerOnPG();
        public Task<ResultOrError<string, string>> GetAllCustomers();
        public Task<ResultOrError<string, string>> GetCustomerById();

    }
}
