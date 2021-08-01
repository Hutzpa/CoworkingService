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
        public Coworking Coworking { get; set; }
        public int SeatsCount { get; set; }
        public List<RoomOccupied> BusyTime { get; set; }
    }

    public class RoomOccupied
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
