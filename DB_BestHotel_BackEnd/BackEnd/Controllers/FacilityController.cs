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
    public class FacilityController : ControllerBase
    {
        private readonly ILogger<FacilityController> _logger;

        public FacilityController(ILogger<FacilityController> logger)
        {
            _logger = logger;
        }
        //获取所有设施信息
        [HttpGet]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public List<FaciltyListResponse> GetAllFacility()
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindFacilityInfo();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //获取某个设施状态
        [HttpPost]
        [Route("api/Status/[controller]")]
        [ApiResponseFilterAttribute]
        public FaciltyStateResponse FindFacilityStatus([FromBody] FaciltyStateRequest value)
        {
            try
            {
                var state = DataAccess.FindFacilityStatus(value.facility_id,value.room_id);
                if (state=="0")
                    return new FaciltyStateResponse { facility_condition = false };
                else if (state == "1")
                    return new FaciltyStateResponse { facility_condition = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //删除某设施
        [HttpPost]
        [Route("api/[controller]/Del")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult DelFacility([FromBody] FaciltyDelRequest value)
        {
            try
            {
                 var date = DataAccess.DelFacility(value.facility_id);
                 if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //添加设施
        [HttpPost]
        [Route("api/[controller]/Add")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult AddFacility([FromBody] FaciltyListResponse value)
        {
            try
            {
                var date = DataAccess.AddFacility(value);
                if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }
        //修改某设施状态
        [HttpPost]
        [Route("api/[controller]/AlertCondition")]
        [ApiResponseFilterAttribute]
        public StatusCodeResult AlertCondition([FromBody] AlertFaciltyConditionRequest value)
        {
            try
            {
                var date = DataAccess.AlertFacilityCondition(value.facility_id,value.room_id,value.facility_condition);
                if (date == true)
                    return new StatusCodeResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new StatusCodeResult(501);
        }

        //获取设施负责人id
        [HttpGet]
        [Route("api/[controller]/Facility/{facility_id}")]
        [ApiResponseFilterAttribute]
        public List<FaciltyStaffResponse> FindFacilityStaff(string facility_id)
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindFacilityStaff(facility_id);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //获取员工负责设施id
        [HttpGet]
        [Route("api/[controller]/Staff/{staff_id}")]
        [ApiResponseFilterAttribute]
        public List<StaffFaciltyResponse> FindStaffFacility(string staff_id)
        {
            if (HttpContext.Session.GetString("IsLogin") != "OK")
                return null;
            try
            {
                var data = DataAccess.FindStaffFacility(staff_id);
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
