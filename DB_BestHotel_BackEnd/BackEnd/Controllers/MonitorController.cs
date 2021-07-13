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
    public class MonitorController : ControllerBase
    {
        private readonly ILogger<MonitorController> _logger;

        public MonitorController(ILogger<MonitorController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public List<MonitorRequest> Get()
        {
            try
            {
                var data = DataAccess.FindMonitorInfo();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        
    }
}
