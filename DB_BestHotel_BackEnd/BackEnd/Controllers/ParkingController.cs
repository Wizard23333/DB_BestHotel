using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.Models;
using BackEnd;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Controllers
{
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;

        public ParkingController(ILogger<ParkingController> logger)
        {
            _logger = logger;
        }
        //获取所有车位信息
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public ShowParkingResponse GetAllParking()
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var date = DataAccess.ShowParkingInfo();
                return new ShowParkingResponse { total = date.Count(), msg = date };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //修改车位信息
        [HttpPost]
        [Route("api/[controller]/Alert")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult AlertParking([FromBody] ParkingMsg value)
        {
            try
            {
                var date = DataAccess.AlertParking(value);
                if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //添加车位
        [HttpPost]
        [Route("api/[controller]/Add")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult AddParkingID([FromBody] ParkingIdRequest value)
        {
            try
            {
                var date = DataAccess.AddParking(value.parking_lot_id);
                if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //删除车位
        [HttpPost]
        [Route("api/[controller]/Del")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult DelParkingID([FromBody] ParkingIdRequest value)
        {
            try
            {
                var date = DataAccess.DelParking(value.parking_lot_id);
                if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //查询某车位信息
        [HttpGet]
        [Route("api/[controller]/Find/{parking_lot_id}")]
        [ApiResponseFilterAttribute]
        public ParkingMsg GetParking(string parking_lot_id)
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var date = DataAccess.FindParkingInfo(parking_lot_id);
                return date;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}