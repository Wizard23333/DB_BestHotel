using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd;

namespace BackEnd.Controllers
{
    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpPost("RoomInfo")]
        [ApiResponseFilterAttribute]
        public List<Room> RoomInfo()
        {
            DataAccess.CreateConn();
            var RoomList = DataAccess.DisplayRoomInfo();
            DataAccess.CloseConn();
            return RoomList;
        }



        [HttpPost("RoomModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomModify(string room_id, int room_price, int room_type, string room_condition,string staff_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyRoomInfo(room_id, room_price, room_type, room_condition,staff_id);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }


        [HttpPost("RoomDelete")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomDelete(string room_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.DeleteRoomInfo(room_id);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }


        [HttpPost("RoomAdd")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomAdd(int room_condition, int room_price, int room_type)//待修改
        {
            DataAccess.CreateConn();
            int Result = DataAccess.AddRoomInfo(room_condition, room_price, room_type);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }


        [HttpPost("RoomTypeInfo")]
        [ApiResponseFilterAttribute]
        public List<RoomTypeInfo> RoomTypeInfo()
        {
            DataAccess.CreateConn();
            var RoomTypeInfoList = DataAccess.DisplayRoomTypeInfo();
            DataAccess.CloseConn();
            /*List<Order> OrderList = new List<Order>();
            OrderList.Add(new Order { order_date = "1" });
            OrderList.Add(new Order { order_date = "1" });*/
            return RoomTypeInfoList;
        }

        [HttpPost("RoomTypeAdd")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomTypeAdd(int room_type, string room_url, string room_explain)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.AddRoomTypeInfo(room_type, room_url, room_explain);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }


        [HttpPost("RoomTypeDelete")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomTypeDelete(int room_type)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.DeleteRoomTypeInfo(room_type);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }
    }
}
