using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS_V2
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public string Features { get; set; }
        public string Availability { get; set; }

        public Room(int roomID, string roomType, string features, string availability)
        {
            RoomID = roomID;
            RoomType = roomType;
            Features = features;
            Availability = availability;
        }
    }

}
