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
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;

        public ParkingController(ILogger<ParkingController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public ShowParkingResponse Get()
        {
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
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Post([FromBody] ParkingMsg value)
        {
            try
            {
                var date = DataAccess.AlertParking(value);
                if(date==true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(400);
        }

        [HttpGet]
        [Route("api/[controller]/{parking_lot_id}")]
        [ApiResponseFilterAttribute]
        public ParkingMsg Get1(string parking_lot_id)
        {
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
