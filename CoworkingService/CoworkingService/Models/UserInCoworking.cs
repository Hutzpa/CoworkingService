using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public class UserInCoworking
    {
        public int CoworkingId { get; set; }
        public virtual Coworking Coworking { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime TotalTimeSpended { get; set; }

        public DateTime UnpayedTimeSpended { get; set; }

        public bool IsActive { get; set; }

        public bool IsBanned { get; set; }
    }
}
