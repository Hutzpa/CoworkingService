using CoworkingService.Constants;
using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Controllers
{
    [Authorize]
    public class CoworkingController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> _userManager;

        public CoworkingController(ApplicationDbContext dbContext, 
            UserManager<User> _userManager)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
        }

        #region CRUD
        [HttpGet]
        [Authorize(Roles = RoleConstants.AdminUser)]
        public async Task<IActionResult> CreateCoworkingAsync(int id)
        {
            if (id == 0)
                return View(new Coworking());
            else
                return View(await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminUser)]
        public async Task<IActionResult> CreateCoworkingAsync(Coworking model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //Добавить поле owner

            if (model.Id == 0)
            {
                await dbContext.Coworkings.AddAsync(model);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                dbContext.Coworkings.Update(model);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Coworking", new { id = model.Id });
        }


        public async Task<IActionResult> CoworkingAsync(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id);

            if (coworking == null)
                return RedirectToAction("Index", "Home");

            return View(coworking);
        }

        public IActionResult CoworkigngsList()
        {
            var user = _userManager.GetUserAsync(User);
            var coworkings = dbContext.
            if(User.IsInRole(RoleConstants.AdminUser))
            return View(new CoworkingListViewModel{
                Coworkings = 
            })
        }

        [Authorize(Roles = RoleConstants.AdminUser)]
        public async Task<IActionResult> CloseCoworkingAsync(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id);
            if (coworking == null)
                return RedirectToAction("Index", "Home");

            coworking.IsOpen = false;
            dbContext.Coworkings.Update(coworking);
            return RedirectToAction("Coworking", new { id = id });
        }

        [Authorize(Roles = RoleConstants.AdminUser)]
        public async Task<IActionResult> DeleteCoworkingAsync(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id);

            if (coworking == null)
                return RedirectToAction("Index", "Home");

            dbContext.Coworkings.Remove(coworking);
            return RedirectToAction()
        }
        #endregion

    }
}
