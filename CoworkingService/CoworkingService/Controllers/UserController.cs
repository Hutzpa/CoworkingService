using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using CoworkingService.Helpers;
using CoworkingService.Constants;
using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CoworkingService.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> _userManager;
        private IConfiguration _config;
        private readonly string DomainName;
        private readonly IFileSaveHelper _fileSaveHelper;

        public UserController(ApplicationDbContext dbContext,
                UserManager<User> _userManager, IConfiguration config, IFileSaveHelper fileSaveHelper)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
            _config = config;
            DomainName = _config.GetSection("Host").GetSection("Address").Value;
            _fileSaveHelper = fileSaveHelper;
        }


        public async Task<IActionResult> AddUserToCoworkingAsync(string userId, int coworkingId)
        {
            dbContext.UsersInCoworkings.Add(new UserInCoworking
            {
                CoworkingId = coworkingId,
                UserId = userId
            });
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Coworking", "Coworking", new { id = coworkingId });
        }

        public async Task<IActionResult> GetUsersInCoworkingAsync(int coworkingId)
        {
          var users = await dbContext.UsersInCoworkings.Where(o => o.CoworkingId == coworkingId).Select(o => o.User).ToListAsync();
            return 
        }
    }
}
