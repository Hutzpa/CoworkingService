using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public class UserInCoworking
    {
        public int CoworkingId { get; set; }
        public virtual Coworking Coworking { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Hours
        /// </summary>
        public int TotalTimeSpended { get; set; }

        /// <summary>
        /// Hours
        /// </summary>
        public int UnpayedHoursSpended { get; set; }

        public bool IsActive { get; set; }

        public bool IsBanned { get; set; }
    }
}
