using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.domain.Model.Transactions
{
    public class TransactionModel
    {
        public string? TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Boolean TransactionStatus { get; set; }
    }
}
