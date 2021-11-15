using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Client
    {
        public virtual string client_id { get; set; }
        public virtual string client_name { get; set; }
        public virtual string client_sex { get; set; }
        public virtual string client_birthday { get; set; }
        public virtual string client_mobile { get; set; }
        public virtual string client_idCard { get; set; }

    }
    public class AlterClientResponse
    {
        public string client_id { get; set; }
        public string client_mobile { get; set; }

    }

}
