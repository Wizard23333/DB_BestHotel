using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class User
    {
        public virtual string UserID { get; set; }
        public virtual string UserPassword { get; set; }
    }
}