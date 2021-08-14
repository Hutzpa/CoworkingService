using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int CoworkingId { get; set; }
        public virtual Coworking Coworking { get; set; }
        public int SeatsCount { get; set; }
        public virtual List<RoomOccupied> BusyTime { get; set; }
    }

    public class RoomOccupied
    {
        public int Id { get; set; }

        public virtual Room Room { get; set; }
        public int RoomId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }



    public class RoomsInCoworking
    {
        public List<Room> Rooms { get; set; }

        public bool DisplayAdminFeatures { get; set; }
    }
}
