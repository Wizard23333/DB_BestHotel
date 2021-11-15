using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class StaffUpdateRequest
    {
        public string staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_sex { get; set; }
        public string staff_age { get; set; }

        public string staff_identity_card_number { get; set; }
        public string staff_address { get; set; }
        public string staff_department { get; set; }
        public string leader_id { get; set; }
        public string staff_position { get; set; }
        public string staff_entry_date { get; set; }
        public string staff_salary { get; set; }
    }
    public class StaffUpdateResponse
    {
        public bool update_state { get; set; }
    }
    public class StaffDeleteRequest
    {
        public string staff_id { get; set; }
    }
    public class StaffDeleteResponse
    {
        public bool delete_state { get; set; }
    }
    public class StaffInfoRequest
    {
        public string staff_id { get; set; }
    }
    public class StaffInfoResponse
    {
        public string staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_sex { get; set; }
        public string staff_age { get; set; }
        public string staff_identity_card_number { get; set; }
        public string staff_address { get; set; }
        public string staff_department { get; set; }
        public string leader_id { get; set; }
        public string staff_position { get; set; }
        public string staff_entry_date { get; set; }
        public string staff_salary { get; set; }
    }
    public class ReturnAllResponse
    {
        public string staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_sex { get; set; }
        public string staff_age { get; set; }
        public string staff_identity_card_number { get; set; }
        public string staff_address { get; set; }
        public string staff_department { get; set; }
        public string leader_id { get; set; }
        public string staff_position { get; set; }
        public string staff_entry_date { get; set; }
        public string staff_salary { get; set; }
    }
    public class StaffAddRequest
    {
        public string staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_sex { get; set; }
        public string staff_age { get; set; }

        public string staff_identity_card_number { get; set; }
        public string staff_address { get; set; }
        public string staff_department { get; set; }
        public string leader_id { get; set; }
        public string staff_position { get; set; }
        public string staff_entry_date { get; set; }
        public string staff_salary { get; set; }
    }
}
