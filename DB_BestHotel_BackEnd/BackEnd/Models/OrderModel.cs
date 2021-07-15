using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class OrderModel
    {
        public string order_id { get; set; }
        public string client_id { get; set; }
        public string dish_order_date { get; set; }
        public decimal amount { get; set; }
        public int state { get; set; }
        public string client_name { get; set; }
        public string client_telephonenumber { get; set; }
        public string dish_name { get; set; }
        public string client_identity_card_number { get; set; }
        public string room_id { get; set; }
        public string room_type { get; set; }
        public string room_order_date { get; set; }
        public decimal stay_time { get; set; }
        public string dish_order_state { get; set; }
    }

    public class RoomOrderResponse
    {
        public string order_id { get; set; }
        public string client_id { get; set; }
        public decimal amount { get; set; }
        public int state { get; set; }
        public string client_name { get; set; }
        public string client_telephonenumber { get; set; }
        public string client_identity_card_number { get; set; }
        public string room_id { get; set; }
        public string room_type { get; set; }
        public string room_order_date { get; set; }
        public decimal stay_time { get; set; }
    }

    public class DishOrderResponse
    {
        public string order_id { get; set; }
        public decimal amount { get; set; }
        public string state { get; set; }
        public string client_name { get; set; }
        public string client_telephonenumber { get; set; }
        public string dish_name { get; set; }
        public string dish_order_date { get; set; }
        public int number { get; set; }

    }
    public class ListInfo
    {
        public int total { get; set; }
        public int pagesize { get; set; }
        public int pagenum { get; set; }
        public List<OrderModel> list { get; set; }
    }
}
