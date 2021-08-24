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
            var coworkings = await _context.Coworkings.Where(o => o.OwnerId == userId).ToListAsync();
            string response = JsonSerializer.Serialize(coworkings);
            return response;
        }
    }
}
