using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class MonitorRoom
    {
        public string room_id { get; set; }
    }
    public class MonitorRequest
    {
        public string monitor_id { get; set; }
        public List<MonitorRoom>rooms { get; set; }

    }
}
