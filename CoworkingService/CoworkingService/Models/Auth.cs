using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
   
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Not less than 2, but no more than 50")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [Remote(action: "IsEmailInUse", controller: "Auth", ErrorMessage = "This email is already in use")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
