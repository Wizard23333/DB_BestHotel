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
            HttpContext.Session.SetString("username", "12345");
            return new StatusCodeResult(402);
        }
        [HttpPost]
        [ApiResponseFilterAttribute]
        public IEnumerable<UserModel> Post()
        {
            string username = HttpContext.Session.GetString("username");
            DataAccess.CreateConn();
            var ID = DataAccess.test();
            DataAccess.CloseConn();
            return Enumerable.ToArray(ID);
        }
    }
}
