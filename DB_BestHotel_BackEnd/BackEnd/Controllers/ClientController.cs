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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public List<Client> Get()
        {
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
        [HttpGet]
        [Route("api/[controller]/{id}")]
        [ApiResponseFilterAttribute]
        public Client Get(string id)
        {
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
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public AlterClientResponse Post([FromBody] AlterClientResponse value)
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
