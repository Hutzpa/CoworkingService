using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot
{
    public class Constants
    {
        public static string Domen { get; set; } = "https://localhost:44335/";

        public static string LoginUrl { get; set; } = "api/AuthApi/"; 

        public static string UserEmail { get; set; }
        public static string UserId { get; set; }
    }
}
