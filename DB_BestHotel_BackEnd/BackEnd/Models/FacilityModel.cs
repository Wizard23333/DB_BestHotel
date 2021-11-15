using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class FaciltyStateRequest
    {
        public string facility_id { get; set; }
        public string room_id { get; set; }

    }
    public class FaciltyStateResponse
    {
        public bool facility_condition { get; set; }
    }
    public class FaciltyDelRequest
    {
        public string facility_id { get; set; }
    }
    public class FaciltyStaffResponse
    {
        public string staff_id { get; set; }
    }
    public class StaffFaciltyResponse
    {
        public string facility_id { get; set; }

    }
    public class FaciltyRoom
    {
        public string room_id { get; set; }
        public  bool facility_condition { get; set; }

    }
    public class FaciltyListResponse
    {
        public string facility_id { get; set; }
        public string facility_name { get; set; }

        public int facility_price { get; set; }
        public string staff_id { get; set; }

        public List<FaciltyRoom> children { get; set; }
    }
    public class AlertFaciltyConditionRequest
    {
        public string facility_id { get; set; }
        public string room_id { get; set; }
        public bool facility_condition { get; set; }

    }


}
