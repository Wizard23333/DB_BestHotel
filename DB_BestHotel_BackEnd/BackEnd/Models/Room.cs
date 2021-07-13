using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Room
    {
        public string room_id { get; set; }
        public int room_price { get; set; }
        public string room_type { get; set; }
        public string room_condition { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string time { get; set; }
    }

    public class RoomTypeInfo
    {
        public int room_price { get; set; }
        public string room_type { get; set; }
        public int room_workable { get; set; }
        public string room_url { get; set; }
        public string room_explain { get; set; }
    }
}
