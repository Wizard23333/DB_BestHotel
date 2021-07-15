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


        //返回房间订单列表

        [HttpPost("RoomOrderListInfo")]
        [ApiResponseFilterAttribute]

        public ListInfo RoomOrderListInfo(int pagesize, int pagenum, string query = "*")
        {
            DataAccess.CreateConn();
            var list = DataAccess.DisplayRoomOrderListInfo(pagesize, pagenum, query);
            DataAccess.CloseConn();
            return list;
        }

        //返回菜品订单列表

        [HttpPost("DishOrderListInfo")]
        [ApiResponseFilterAttribute]
        public ListInfo DishOrderListInfo(string query = "*")
        {
            DataAccess.CreateConn();
            var list = DataAccess.DisplayDishOrderListInfo(query);
            DataAccess.CloseConn();
            return list;
        }

        //查询房间订单信息

        [HttpPost("OrderQuery")]
        [ApiResponseFilterAttribute]

        public ActionResult<OrderModel> OrderQuery(string order_id)
        {
            DataAccess.CreateConn();
            var order = DataAccess.QueryOrderInfo(order_id);
            DataAccess.CloseConn();
            return order;
        }

        //取消房间订单

        [HttpPost("OrderCancel")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult OrderCancel(string order_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyOrderInfo(order_id, 3);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //修改订单信息

        [HttpPost("OrderModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult OrderModify(string order_id, int state)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyOrderInfo(order_id, state);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //修改房间订单信息

        [HttpPost("RoomOrderModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomOrderModify(string order_id, int state)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyRoomOrderInfo(order_id, state);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //修改菜品订单信息

        [HttpPost("OrderDishModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult OrderDishModify(string order_id, int state)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyDishOrderInfo(order_id, state);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }
        //预定房间

        [HttpPost("RoomReserve")]
        [ApiResponseFilterAttribute]

        public RoomOrderResponse RoomReserve(string client_id, string order_date, string room_type, decimal amount, string client_name, string client_identity_card_number, string room_order_date, decimal stay_time = 1, string client_telephonenumber = null)
        {
            DataAccess.CreateConn();
            RoomOrderResponse Result = DataAccess.AddRoomOrder(client_id, order_date, room_type, amount, client_name, client_identity_card_number, room_order_date, stay_time, client_telephonenumber);
            DataAccess.CloseConn();
            return Result;
        }


        //点餐

        [HttpPost("DishReserve")]
        [ApiResponseFilterAttribute]
        public DishOrderResponse DishReserve(string client_name, string client_id, string dish_name, string client_telephonenumber, string dish_order_date, int number = 1)
        {
            DataAccess.CreateConn();
            DishOrderResponse Result = DataAccess.AddDishOrder(client_name, client_id, dish_name, client_telephonenumber, dish_order_date, number);
            DataAccess.CloseConn();
            return Result;

        }
    }
}
