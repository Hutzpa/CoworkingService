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
        public static string CoworkingsUrl { get; set; } = "api/CoworkingApi/"; 

        public static string UserEmail { get; set; }
        public static string UserId { get; set; }
    }

    public class Coworking
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Address { get; set; }
        public CoworkingType CoworkingType { get; set; }
        public CoworkingPaymentType PaymentType { get; set; }
     
        public decimal Cost { get; set; }
        public bool IsOpen { get; set; } = true;
        public string Description { get; set; }
        public int PeopleCurrentlyIn { get; set; } = 0;
        public string OwnerId { get; set; }

        //public virtual List<Picture> Photos { get; set; }

        ////public virtual User Owner { get; set; }

        //public virtual List<Room> Rooms { get; set; }
        //public virtual List<UserInCoworking> InCoworking { get; set; }
    }

    public enum CoworkingType
    {
        Office, Street, Kids, Crafting, Art
    }

    public enum CoworkingPaymentType
    {
        Hour, Day, Week
    }
}
