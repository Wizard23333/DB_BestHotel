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
    [Route("api/[controller]")]
    public class testController : ControllerBase
    {
        private readonly ILogger<testController> _logger;

        public testController(ILogger<testController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Get()
        { 
            return new StatusCodeResult(402);
        }
        [HttpPost]
        [ApiResponseFilterAttribute]
        public IEnumerable<User> Post()
        {
            DataAccess.CreateConn();
            var ID = DataAccess.test();
            DataAccess.CloseConn();
            return Enumerable.ToArray(ID);
        }
    }
}
