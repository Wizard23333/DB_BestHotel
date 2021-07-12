using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class User
    {
        public virtual string user_id { get; set; }
        public virtual string user_name { get; set; }
        public virtual string user_password { get; set; }
        public virtual string user_type { get; set; }

        public virtual string security_q{ get; set; }

        public virtual string s_q_answer { get; set; }

    }
}