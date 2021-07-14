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
    public class StaffController : ControllerBase 
    {

        [HttpPost("StaffInfo")]
        [ApiResponseFilterAttribute]
        //查找员工信息 成功返回此员工的所有信息外加他的上司id，不成功返回null
        public StaffInfoResponse Post([FromBody] StaffInfoRequest value)
        {
            try
            {
                if (DataAccess.IsStaffExist(value.staff_id) == true)
                {
                    var date = DataAccess.FindStaffInfo(value.staff_id);
                    string date_leader_id = DataAccess.FindUperId(value.staff_id);
                    return new StaffInfoResponse { staff_name = date.staff_name, staff_sex = date.staff_sex, staff_age = date.staff_age, staff_identity_card_number = date.staff_identity_card_number, staff_address = date.staff_address, staff_department = date.staff_department, leader_id = date_leader_id, staff_position = date.staff_position, staff_entry_date = date.staff_entry_date, staff_salary = date.staff_salary };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //删除员工，删除成功返回true 不成功返回false
        [HttpPost("StaffDelete")]
        [ApiResponseFilterAttribute]
        public StaffDeleteResponse Post([FromBody] StaffDeleteRequest value)
        {
            try
            {
                if (DataAccess.IsStaffExist(value.staff_id) == true)
                {
                    if (DataAccess.DeleteStaff(value.staff_id) == "0")
                    {
                        return new StaffDeleteResponse { delete_state = false };
                    }
                    else
                    {
                        return new StaffDeleteResponse { delete_state = true };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        //修改员工信息
        [HttpPost("StaffUpdate")]
        [ApiResponseFilterAttribute]
        public StaffUpdateResponse Post([FromBody] StaffUpdateRequest value)
        {
            try
            {
                if (DataAccess.IsStaffExist(value.staff_id) == true)
                {
                    if (DataAccess.StaffUpdate(value) == false)
                    {
                        return new StaffUpdateResponse { update_state = false };
                    }
                    else
                    {
                        return new StaffUpdateResponse { update_state = true };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        //无请求参数 返回所有信息
        [HttpPost("ReturnAll")]
        [ApiResponseFilterAttribute]
        public List<ReturnAllResponse> Post()
        {
            return DataAccess.ReturnAll();
        }
        //添加员工
        [HttpPost("StaffAdd")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult Post([FromBody] StaffAddRequest value)
        {
            try
            {
                if (DataAccess.IsStaffExist(value.staff_id) != true)
                {

                    return new StatusCodeResult(200);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        
    }
}
