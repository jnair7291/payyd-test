using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.domain.Model.Wallet
{
    public class WalletModel
    {
        public string? CardId { get; set; }

        public string CardType { get; set; }

        public string CardLast4Digit { get; set; }

        public Boolean IsCreditAvaiable { get; set; }

        public decimal CreditAmount { get; set; }

        public Boolean IsCardOnFile { get; set; }

        public string? CustomerId { get; set; }
    }
}
