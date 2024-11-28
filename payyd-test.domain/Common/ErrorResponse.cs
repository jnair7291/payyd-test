using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.domain.Common
{
    public class ErrorResponse
    {
        public string? Message { get; }

        public Errors Reason { get; }

        public ErrorResponse(Errors reason, string message)
        {
            this.Reason = reason;
            Message = message;
        }
    }
}
