using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class IdentityRequest
    {
        public string user_id { get; set; }
        public string user_password { get; set; }
    }
    public class IdentityResponse
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_type { get; set; }
    }
}
