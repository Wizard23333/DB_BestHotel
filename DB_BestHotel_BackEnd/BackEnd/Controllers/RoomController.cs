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

        //查询所有房间信息
        [HttpPost("RoomInfo")]
        [ApiResponseFilterAttribute]
        public List<RoomModel> RoomInfo()
        {
            DataAccess.CreateConn();
            var RoomList = DataAccess.DisplayRoomInfo();
            DataAccess.CloseConn();
            return RoomList;
        }

        //修改房间信息

        [HttpPost("RoomModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomModify(string room_id, int room_price, string room_type, string room_condition,string staff_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyRoomInfo(room_id, room_price, room_type, room_condition,staff_id);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //删除房间信息

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

        //增加房间信息

        [HttpPost("RoomAdd")]
        [ApiResponseFilterAttribute]

        public decimal RoomAdd(string room_condition, string staff_id, decimal room_price, string room_type)
        {
            DataAccess.CreateConn();
            decimal Result = DataAccess.AddRoomInfo(room_condition, staff_id, room_price, room_type);
            DataAccess.CloseConn();
            return Result;
        }

        //查询房间类型信息

        [HttpPost("RoomTypeInfo")]
        [ApiResponseFilterAttribute]
        public List<RoomTypeInfo> RoomTypeInfo()
        {
            DataAccess.CreateConn();
            var RoomTypeInfoList = DataAccess.DisplayRoomTypeInfo();
            DataAccess.CloseConn();
            return RoomTypeInfoList;
        }

        //增加房间类型信息

        [HttpPost("RoomTypeAdd")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomTypeAdd(string room_type, string room_url, string room_explain)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.AddRoomTypeInfo(room_type, room_url, room_explain);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //修改房间类型信息

        [HttpPost("RoomTypeModify")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomTypeModify(string room_type, string room_explain)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.ModifyRoomTypeInfo(room_type, room_explain);
            DataAccess.CloseConn();
            if (Result == 1)
                return new StatusCodeResult(200);
            else return new StatusCodeResult(404);
        }

        //删除房间类型信息

        [HttpPost("RoomTypeDelete")]
        [ApiResponseFilterAttribute]

        public StatusCodeResult RoomTypeDelete(string room_type)
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
