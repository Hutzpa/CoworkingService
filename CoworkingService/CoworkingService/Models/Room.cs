using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        [NotMapped]
        public List<RoomOccupiedEvent> RoomOccupations { get; set; }

     
    }

    public class RoomOccupied
    {
        public RoomOccupied()
        {

        }

        public RoomOccupied(int RoomId)
        {
            this.RoomId = RoomId;
        }

        public int Id { get; set; }

        public virtual Room Room { get; set; }
        public int RoomId { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }

        [Required]
        public DateTime From { get; set; } = DateTime.Now;
        [Required]
        public DateTime To { get; set; } = DateTime.Now;
    }

    public class RoomOccupiedEvent
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Start { get; set; }
        public string End { get; set; }
        public string ThemeColor { get; set; } = "green";
        public bool IsFullDay { get; set; } = false;
    }

    public class RoomsInCoworking
    {
        public List<Room> Rooms { get; set; }

        public bool DisplayAdminFeatures { get; set; }
    }
}
