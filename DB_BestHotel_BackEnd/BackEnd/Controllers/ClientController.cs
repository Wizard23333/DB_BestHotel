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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
        //获取所有客户信息
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public List<Client> GetAllClient()
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindClientInfo();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //获取某客户信息
        [HttpGet]
        [Route("api/[controller]/{id}")]
        [ApiResponseFilterAttribute]
        public Client GetClient(string id)
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindClientInfo(id);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //修改客户信息
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public AlterClientResponse AlterClient([FromBody] AlterClientResponse value)
        {
            try
            {
                if(DataAccess.AlterClient(value.client_id,value.client_mobile)==true)
                    return new AlterClientResponse{ client_id= value.client_id, client_mobile= value.client_mobile };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

    }
}
