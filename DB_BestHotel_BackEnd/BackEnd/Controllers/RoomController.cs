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
    [Route("api/[controller]")]
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
            /*List<Order> OrderList = new List<Order>();
            OrderList.Add(new Order { order_date = "1" });
            OrderList.Add(new Order { order_date = "1" });*/
            return RoomList;
        }
    }
}
