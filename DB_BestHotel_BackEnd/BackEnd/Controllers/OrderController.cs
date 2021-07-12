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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {



        [HttpPost("Post")]
        [ApiResponseFilterAttribute]
        public List<Order> Post( out int total,string query = "*")
        {
            DataAccess.CreateConn();
            var OrderList = DataAccess.DisplayOrderInfo(query);
            total= OrderList.Count();
            DataAccess.CloseConn();
            return OrderList;
        }


        [HttpPost("OrderQuery")]
        [ApiResponseFilterAttribute]

        public Order OrderQuery(string order_id)
        {
            DataAccess.CreateConn();
            var order = DataAccess.Query(order_id);
            DataAccess.CloseConn();
            return order;
        }


        [HttpPost("OrderModify")]
        [ApiResponseFilterAttribute]

        public bool OrderModify(string order_id)
        {
            DataAccess.CreateConn();
            int Result = DataAccess.Modify(order_id);
            DataAccess.CloseConn();
            if (Result == 1)
                return true;
            else return false;
        }
    }
}
