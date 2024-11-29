using payyd_test.application.Interfaces;
using payyd_test.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.application.Services
{
    public class TransactionService :ITransactionService
    {
        public ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ResultOrError<string, ErrorResponse>> MakePaymentAsync(string customerId)
        {
            return await _transactionRepository.MakePaymentAsync(customerId);
        }
    }
}
