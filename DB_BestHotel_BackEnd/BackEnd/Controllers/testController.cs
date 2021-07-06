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
        public string Get()
        {
            return "HelloWorld";
        }
        [HttpPost]
        public IEnumerable<User> Post()
        {
            DataAccess.CreateConn();
            string ID = DataAccess.test();
            DataAccess.CloseConn();
            return Enumerable.Range(0,1).Select(index => new User
            {
                UserID=ID,
            })
            .ToArray();
        }
    }
}
