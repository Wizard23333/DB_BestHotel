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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public IdentityResponse Post([FromBody] IdentityRequest value)
        {
            try
            {
                if (DataAccess.IsUserExist(value.user_id, value.user_password) == true)
                {
                    var data = DataAccess.FindUserInfo(value.user_id);
                    return new IdentityResponse { user_id = data.user_id, user_name = data.user_name, user_type = data.user_type };
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/[controller]/Register/Client")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Post([FromBody] RegClientRequest value)
        {
                try
                {
                    User data1 = new User { user_id = value.client_id, user_name = value.client_name, user_password = value.password, user_type = "1", security_q = value.security_q, s_q_answer = value.s_q_answer };
                    Client data2 = new Client { client_id = value.client_id, client_name = value.client_name, client_sex = value.client_sex, client_birthday = value.client_birthday, client_mobile = value.client_mobile, client_idCard = value.client_idCard };
                    if (DataAccess.AddUser(data1)&& DataAccess.AddClient(data2))
                        return new StatusCodeResult(200);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return null;
        }
        [HttpPost]
        [Route("api/[controller]/Register/Staff")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Post([FromBody] RegStaffRequest value)
        {
            try
            {
                User data1 = new User { user_id = value.staff_id, user_name = value.staff_name, user_password = value.password, user_type = "2", security_q = value.security_q, s_q_answer = value.s_q_answer };
                Staff data2 =value;
                if (DataAccess.AddUser(data1) && DataAccess.AddStaff(data2))
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        [HttpPost]
        [Route("api/[controller]/Forget/{id}")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Post(string id,[FromBody] ForgetRequest value)
        {
            try
            {
                var data = DataAccess.FindUserInfo(id);
                if ((data.s_q_answer == value.s_q_answer)&&(DataAccess.AlterUserPassword(id,value.user_password)==true))
                 return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        [HttpGet]
        [Route("api/[controller]/Forget/{id}")]
        [ApiResponseFilterAttribute]
        public ForgetResponse Get(string id)
        {
            try
            {
                var data = DataAccess.FindUserInfo(id);
                return new ForgetResponse { user_id=id,security_q=data.security_q };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
