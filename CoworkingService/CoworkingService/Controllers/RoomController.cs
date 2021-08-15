using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CoworkingService.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CoworkingService.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> BookTheRoomAsync(RoomOccupied model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if(model.From > model.To)
            {
                ModelState.AddModelError("From","Correct the time");
                ModelState.AddModelError("To", "Correct the time");
                return View(model);
            }
            var allBookingsForThisRoom = await dbContext.RoomOccupieds.Where(o => o.RoomId == model.RoomId && o.From > DateTime.Now).ToListAsync();
            if (IsRoomBusyAtThisTime(allBookingsForThisRoom, model.From, model.To))
            {
                ModelState.AddModelError("From", "Room is already booked on this time");
                ModelState.AddModelError("To", "Room is already booked on this time");
                return View(model);
            }
            dbContext.RoomOccupieds.Add(model);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Room", new { id = model.RoomId });
        }

        private bool IsRoomBusyAtThisTime(List<RoomOccupied> occupations,DateTime from, DateTime to)
        {
            bool isBusy = false;
            foreach(var occup in occupations)
            {
                if(occup.From >= from && occup.To <= to ){
                    isBusy = true;
                    break;
                }
            }
            return isBusy;
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


        public async Task<IActionResult> GetAllBookingsAsync(int roomId)
        {
            var bookings =await dbContext.RoomOccupieds.Where(o => o.RoomId == roomId).Select(o => new RoomOccupiedEvent
            {
                Title = o.Title,
                Description = o.Description,
                StartDate = o.From.ToString("MM/dd/yyyy"),
                EndDate = o.To.ToString("MM/dd/yyyy")
            }).ToListAsync();

            return new JsonResult(bookings);
        }
    }
}
