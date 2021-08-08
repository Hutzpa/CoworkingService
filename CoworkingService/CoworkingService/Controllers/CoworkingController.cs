using CoworkingService.Constants;
using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

namespace CoworkingService.Controllers
{
    [Authorize]
    public class CoworkingController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> _userManager;
        private IConfiguration _config;
        private readonly string DomainName;
        private readonly IFileSaveHelper _fileSaveHelper;

        public CoworkingController(ApplicationDbContext dbContext,
                UserManager<User> _userManager, IConfiguration config, IFileSaveHelper fileSaveHelper)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
            _config = config;
            DomainName = _config.GetSection("Host").GetSection("Address").Value;
            _fileSaveHelper = fileSaveHelper;
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

            if (model.Id == 0)
            {
                model.Owner = await _userManager.GetUserAsync(User);
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

        //[Authorize(Roles = RoleConstants.AdminUser)]
        public async Task<IActionResult> CloseCoworkingAsync(int id, string whereCameFrom)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id);
            if (coworking == null)
                return RedirectToAction("Index", "Home");

            var queryString = Request.QueryString;
            coworking.IsOpen = !coworking.IsOpen;
            dbContext.Coworkings.Update(coworking);
            await dbContext.SaveChangesAsync();
            if (String.IsNullOrEmpty(whereCameFrom))
                return RedirectToAction("Coworking", new { id = id });
            return Redirect(DomainName + whereCameFrom);
        }


        [HttpGet]
        public async Task<IActionResult> CoworkingAsync(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == id);

            if (coworking == null)
                return RedirectToAction("Index", "Home");

            return View(coworking);
        }

        public async Task<IActionResult> CoworkingsListAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var coworking = new List<Coworking>();
            if (User.IsInRole(RoleConstants.AdminUser))
                coworking = await dbContext.Coworkings.Where(o => o.OwnerId == user.Id).ToListAsync();
            else
                coworking = await dbContext.UsersInCoworkings.Where(o => o.UserId == user.Id).Select(o => o.Coworking).ToListAsync();
            return View(new CoworkingListViewModel
            {
                Coworkings = coworking
            });
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
            await dbContext.SaveChangesAsync();

            return RedirectToAction("CoworkingsList" );
        }
        #endregion



        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadPhotosCoworking(List<IFormFile> files,int coworkingId)
        {
            if (files == null || files.Count == 0)
                return Content("file not selected");
            
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    dbContext.Pictures.Add(new Picture
                    {
                        Path = await _fileSaveHelper.SaveImage(formFile),
                        CoworkingId = coworkingId,
                    });
                }
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Coworking", new { id = coworkingId });
        }
    }
}
