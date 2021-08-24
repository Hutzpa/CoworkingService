using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoworkingService.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {

        private SignInManager<User> _signInManager;
        private ApplicationDbContext _context;

        public AuthApiController(ApplicationDbContext context,
            SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
        }
        // GET: api/Auth
        [HttpGet]
        public async Task<string> GetAsync(string login, string password)
        {
            var res = await _signInManager.PasswordSignInAsync(login, password, false, false);
            if (res.Succeeded)
            {
                User user = await _context.Users.FirstOrDefaultAsync(o => o.UserName == login);
                return user.Id;
            }
            return "";
        }
    }
}
