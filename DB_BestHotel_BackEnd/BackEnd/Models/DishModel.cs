using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackEnd.Models
{
    public class DishUpdateRequest
    {
        public string dish_id { get; set; }
        public string dish_explain { get; set; }
        public string dish_price { get; set; }
        public string dish_name { get; set; }
    }
    public class DishUpdateResponse
    {
        public bool dish_update { get; set; }
    }
    public class DishAddRequest
    {
        public string dish_id { get; set; }
        public string dish_explain { get; set; }
        public string dish_price { get; set; }
        public string dish_name { get; set; }
        public string dish_picture { get; set; }
    }
    public class DishDeleteRequest
    {
        public string dish_id { get; set; }
    }
    public class DishInfo
    {
        public string dish_id { get; set; }
        public string dish_explain { get; set; }
        public string dish_price { get; set; }
        public string dish_name { get; set; }
    }
}
