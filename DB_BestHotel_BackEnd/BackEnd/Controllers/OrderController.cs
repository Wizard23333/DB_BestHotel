using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.Models;
using BackEnd;


namespace BackEnd.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {


        //Post /api/Order/OrderInfo
        //返回数据库中所有的订单信息

        [HttpPost("OrderInfo")]
        [ApiResponseFilterAttribute]
        public List<Order> OrderInfo(string query = "*")
        {
            DataAccess.CreateConn();
            var OrderList = DataAccess.DisplayOrderInfo(query);        
            DataAccess.CloseConn();
            /*List<Order> OrderList = new List<Order>();
            OrderList.Add(new Order { order_date = "1" });
            OrderList.Add(new Order { order_date = "1" });*/
            return OrderList;
        }

        //Post /api/Order/OrderQuery
        //返回数据库中对应order_id的订单信息

        [HttpPost("OrderQuery")]
        [ApiResponseFilterAttribute]

        public ActionResult<Order> OrderQuery(string order_id)
        {
            DataAccess.CreateConn();
            var order = DataAccess.QueryOrderInfo(order_id);
            DataAccess.CloseConn();
            return order;
        }

        //Post /api/Order/OrderModify
        //修改数据库中对应order_id的订单信息

        [HttpPost("OrderModify")]
        [ApiResponseFilterAttribute]

        public bool OrderModify(string order_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyOrderInfo(order_id);
            DataAccess.CloseConn();
            if (Result == 1)
                return true;
            else return false;
        }

        //Post /api/Order/RoomReserve
        //修改数据库中对应order_id的订单信息

        [HttpPost("RoomReserve")]
        [ApiResponseFilterAttribute]

        public void RoomReserve(string user_id, string order_date, string room_type, string client_telephonenumber = null, int stay_time = 1)
        {
            DataAccess.CreateConn();
            DataAccess.AddRoomOrder(user_id, order_date, room_type, client_telephonenumber, stay_time);
            DataAccess.CloseConn();
        }


        [HttpPost("DishReserve")]
        [ApiResponseFilterAttribute]
        public void DishReserve(string user_id, string dish_name,  int number = 1)
        {
            DataAccess.CreateConn();
            DataAccess.AddDishOrder(user_id, dish_name, number);
            DataAccess.CloseConn();

        }
    }
}
