using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CoworkingService.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoworkingService.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> _userManager;
        private IConfiguration _config;
        private readonly string DomainName;
        private readonly IFileSaveHelper _fileSaveHelper;

        public RoomController(ApplicationDbContext dbContext,
                UserManager<User> _userManager, IConfiguration config, IFileSaveHelper fileSaveHelper)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
            _config = config;
            DomainName = _config.GetSection("Host").GetSection("Address").Value;
            _fileSaveHelper = fileSaveHelper;
        }


        public async Task<IActionResult> AddRoomAsync(int id, int seatsCount, int coworkingId)
        {
            if (id == 0)
            {
                await dbContext.Rooms.AddAsync(new Room
                {
                    CoworkingId = coworkingId,
                    SeatsCount = seatsCount
                });
            }
            else
            {
                var room = await dbContext.Rooms.FirstOrDefaultAsync(o => o.Id == id);
                room.SeatsCount = seatsCount;
                dbContext.Rooms.Update(room);
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Coworking", "Coworking", new { id = coworkingId });
        }


        public async Task<IActionResult> RoomAsync(int id)
        {
            var room = await dbContext.Rooms.FirstOrDefaultAsync(o => o.Id == id);

            return View(room);
        }

        public async Task<IActionResult> DeleteRoomAsync(int id)
        {
            var room = await dbContext.Rooms.FirstOrDefaultAsync(o => o.Id == id);
            dbContext.Rooms.Remove(room);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Coworking", "Coworking", new { id = room.CoworkingId });
        }
    }
}
