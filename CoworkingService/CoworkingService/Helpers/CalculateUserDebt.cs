using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CoworkingService.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoworkingService.Helpers
{
    public class CalculateUserDebt
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> _userManager;
        private IConfiguration _config;
        private readonly string DomainName;
        private readonly IFileSaveHelper _fileSaveHelper;

        public CalculateUserDebt(ApplicationDbContext dbContext,
                UserManager<User> _userManager, IConfiguration config, IFileSaveHelper fileSaveHelper)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
            _config = config;
            DomainName = _config.GetSection("Host").GetSection("Address").Value;
            _fileSaveHelper = fileSaveHelper;
        }

        public async Task<decimal> CalculateAsync(int coworkingId, string userId)
        {
            var uic = await dbContext.UsersInCoworkings.FirstOrDefaultAsync(o => o.UserId == userId && o.CoworkingId == coworkingId);
            //Calculation
            return 0m;
        }
    }
}
