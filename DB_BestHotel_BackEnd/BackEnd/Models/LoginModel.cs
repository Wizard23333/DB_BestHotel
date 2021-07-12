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
    public class RegClientRequest:Client
    {
        public string password { get; set; }

        public string security_q { get; set; }

        public string s_q_answer { get; set; }
    }
    public class RegisterResponse
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_type { get; set; }
    }
    public class ForgetResponse
    {
        public string user_id { get; set; }
        public string security_q { get; set; }
    }
    public class ForgetRequest
    {
        public string s_q_answer { get; set; }
        public string user_password { get; set; }
    }
    public class Staff
    {
        public virtual string staff_id { get; set; }
        public virtual string staff_name { get; set; }
        public virtual string staff_sex { get; set; }
        public virtual string staff_age { get; set; }
        public virtual string staff_identity_card_number { get; set; }
        public virtual string staff_address { get; set; }
        public virtual string staff_department { get; set; }
        public virtual string staff_position { get; set; }
        public virtual string staff_entry_date { get; set; }
        public virtual string staff_salary { get; set; }

    }
    public class RegStaffRequest : Staff
    {
        public string password { get; set; }

        public string security_q { get; set; }

        public string s_q_answer { get; set; }
    }
}
