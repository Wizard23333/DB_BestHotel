using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class ParkingMsg
    {
        public string parking_lot_id { get; set; }
        public string user_id { get; set; }
        public string car_number { get; set; }

    }
    public class ShowParkingResponse
    {
        public int total { get; set; }
        public List<ParkingMsg> msg { get; set; }

    }
    public class AlertParkingRequest
    {
        public int total { get; set; }
        public List<ParkingMsg> msg { get; set; }

    }
}