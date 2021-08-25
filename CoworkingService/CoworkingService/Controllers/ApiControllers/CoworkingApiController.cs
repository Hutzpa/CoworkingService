using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoworkingService.Controllers.ApiControllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CoworkingApiController : ControllerBase
    {

        private ApplicationDbContext _context;

        public CoworkingApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("coworkings")]
        public async Task<string> GetCoworkingsAsync(string userId)
        {
            try
            {
                var coworkings = _context.Coworkings.Where(o => o.OwnerId == userId).Select(o => new
                {
                    Id = o.Id,
                    Name = o.Name,
                    Address = o.Address,
                    CoworkingType = o.CoworkingType,
                    PaymentType = o.PaymentType,
                    Cost = o.Cost,
                    IsOpen = o.IsOpen,
                    Description = o.Description,
                    PeopleCurrentlyIn = o.PeopleCurrentlyIn,
                    OwnerId = o.OwnerId
                });
                string response = JsonSerializer.Serialize(coworkings);
                return response;
            }
            catch (Exception ex)
            {

            }
            return "0";
        }


        [HttpGet("countPeople")]
        public async Task<int> CountPeopleInCoworkingAsync(int coworkingId, int peopleToCome)
        {
            var coworking = await _context.Coworkings.FirstOrDefaultAsync(o => o.Id == coworkingId);
            coworking.PeopleCurrentlyIn += peopleToCome;
            _context.Coworkings.Update(coworking);
            await _context.SaveChangesAsync();

            return coworking.PeopleCurrentlyIn;
        }
    }
}
