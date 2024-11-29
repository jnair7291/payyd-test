using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payyd_test.domain.Model.Users
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string Useremailid { get; set; }
        public string? PGCustomerId { get; set; }

        public string? UserName { get; set; }

    }
}
