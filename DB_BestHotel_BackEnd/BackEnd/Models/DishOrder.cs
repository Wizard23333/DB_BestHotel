using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class DishOrder
    {
        public string user_id { get; set; }
        public string dish_name { get; set; }
        public int number { get; set; }
    }
}
