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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [ApiResponseFilterAttribute]
        public IdentityResponse Post([FromBody] IdentityRequest value)
        {
            try
            {
                if (DataAccess.IsUserExist(value.user_id, value.user_password) == true)
                {
                    var date = DataAccess.FindUserInfo(value.user_id);
                    return new IdentityResponse { user_id = date.user_id, user_name = date.user_name, user_type = date.user_type };
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
