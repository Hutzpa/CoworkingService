using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string[] Photo { get; set; }  

        public List<Coworking> Coworkings { get; set; }
        public List<UserInCoworking> InCoworkings { get; set; }
    }


    public class UserViewModel
    {

    }
}
