﻿using Microsoft.AspNetCore.Http;
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


        public virtual List<Picture> Photos { get; set; }  
        public virtual List<Coworking> Coworkings { get; set; }
        public virtual List<UserInCoworking> InCoworkings { get; set; }

    }


    public class UserViewModel
    {

    }
}
