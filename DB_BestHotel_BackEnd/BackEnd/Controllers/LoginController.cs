using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.Models;
using BackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

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
        //用户登录
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        
        public IdentityResponse Login([FromBody] IdentityRequest value)
        {
            try
            {
                if (DataAccess.IsUserExist(value.user_id, value.user_password) == true)
                {
                    var data = DataAccess.FindUserInfo(value.user_id);
                    HttpContext.Session.SetString("IsLogin", "OK");
                    return new IdentityResponse { user_id = data.user_id, user_name = data.user_name, user_type = data.user_type };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //客户注册
        [HttpPost]
        [Route("api/[controller]/Register/Client")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Client([FromBody] RegClientRequest value)
        {
            try
            {
                UserModel data1 = new UserModel { user_id = value.client_id, user_name = value.client_name, user_password = value.password, user_type = "1", security_q = value.security_q, s_q_answer = value.s_q_answer };
                Client data2 = new Client { client_id = value.client_id, client_name = value.client_name, client_sex = value.client_sex, client_birthday = value.client_birthday, client_mobile = value.client_mobile, client_idCard = value.client_idCard };
                if (DataAccess.AddUser(data1) && DataAccess.AddClient(data2))
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //员工注册
        [HttpPost]
        [Route("api/[controller]/Register/Staff")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult RegisterStaff([FromBody] RegStaffRequest value)
        {
            try
            {
                UserModel data1 = new UserModel { user_id = value.staff_id, user_name = value.staff_name, user_password = value.password, user_type = "2", security_q = value.security_q, s_q_answer = value.s_q_answer };
                Staff data2 = value;
                if (DataAccess.AddUser(data1) && DataAccess.AddStaff(data2))
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //修改密码
        [HttpPost]
        [Route("api/[controller]/Forget/{id}")]
        [ApiResponseFilterAttribute]
        
        public StatusCodeResult Forget(string id, [FromBody] ForgetRequest value)
        {
            try
            {
                var data = DataAccess.FindUserInfo(id);
                if ((data.s_q_answer == value.s_q_answer) && (DataAccess.AlterUserPassword(id, value.user_password) == true))
                    return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //获取密保问题
        [HttpGet]
        [Route("api/[controller]/Forget/{id}")]
        [ApiResponseFilterAttribute]
        public ForgetResponse GetForgetQusetion(string id)
        {
            try
            {
                var data = DataAccess.FindUserInfo(id);
                return new ForgetResponse { user_id = id, security_q = data.security_q };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
