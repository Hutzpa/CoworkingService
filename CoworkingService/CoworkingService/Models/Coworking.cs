using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public enum CoworkingType
    {
        Office,Street,Kids,Crafting,Art
    }

    public enum CoworkingPaymentType
    {
        Hour,Day,Week
    }
    public class Coworking
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Address { get; set; }
        public CoworkingType CoworkingType { get; set; }
        public CoworkingPaymentType PaymentType { get; set; }
        [Required]
        [Range(0,Double.MaxValue)]
        public decimal Cost { get; set; }
        public bool IsOpen { get; set; } = true;
        public string Description { get; set; }
        public string[] Photos { get; set; }

        public List<Room> Rooms { get; set; }
    }

    public class CoworkingListViewModel
    {
        public List<Coworking> Coworkings { get; set; }

        public bool IsAdminViewing { get; set; }
    }

   
}
