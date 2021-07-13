<<<<<<< Updated upstream
﻿
=======
﻿using System;
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
    public class FacilityController : ControllerBase
    {
        private readonly ILogger<FacilityController> _logger;

        public FacilityController(ILogger<FacilityController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("api/[controller]")]
        [ApiResponseFilterAttribute]
        public FaciltyStateResponse Post([FromBody] FaciltyStateRequest value)
        {
            try
            {
                var state = DataAccess.FindFacilityState(value.facility_id,value.room_id);
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
        [HttpGet]
        [Route("api/[controller]/facility/{facility_id}")]
        [ApiResponseFilterAttribute]
        public List<FaciltyStaffResponse> Get(string facility_id)
        {
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
        [HttpGet]
        [Route("api/[controller]/staff/{staff_id}")]
        [ApiResponseFilterAttribute]
        public List<StaffFaciltyResponse> Gett(string staff_id)
        {
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
>>>>>>> Stashed changes
