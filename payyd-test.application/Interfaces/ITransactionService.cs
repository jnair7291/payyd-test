using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.application.Interfaces
{
    public interface ITransactionService
    {
        public Task<ResultOrError<string, ErrorResponse>> MakePaymentAsync(string customerId);
       
    }
}
