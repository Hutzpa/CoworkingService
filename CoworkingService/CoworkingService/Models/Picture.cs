using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int CoworkingId { get; set; }
        public virtual Coworking Coworking { get; set; }


    }
}
