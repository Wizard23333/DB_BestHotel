using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Order
    {
        public string order_id { get; set; }
        public string client_id { get; set; }
        public string order_date { get; set; }
        public decimal amount { get; set; }
        public int state { get; set; }
        
    }

    
}
