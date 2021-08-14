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
    public interface ICalculateUserDebt
    {
        Task<decimal> CalculateAsync(int coworkingId, string userId);
    }

    public class CalculateUserDebt : ICalculateUserDebt
    {
        private ApplicationDbContext dbContext;

        public CalculateUserDebt(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<decimal> CalculateAsync(int coworkingId, string userId)
        {
            var uic = await dbContext.UsersInCoworkings.FirstOrDefaultAsync(o => o.UserId == userId && o.CoworkingId == coworkingId);
            //Calculation
            if (uic.UnpayedHoursSpended < 1)
                return 0;

            var coworking = await dbContext.Coworkings.FirstOrDefaultAsync(o => o.Id == coworkingId);
            decimal toPay = 0;

            switch (coworking.PaymentType)
            {
                case CoworkingPaymentType.Hour:
                    {
                        toPay = uic.UnpayedHoursSpended * coworking.Cost;
                        break;
                    }
                case CoworkingPaymentType.Day:
                    {
                        toPay = (uic.UnpayedHoursSpended / 8) * coworking.Cost;
                        break;
                    }
                case CoworkingPaymentType.Week:
                    {
                        toPay = ((uic.UnpayedHoursSpended / 8) / 7) * coworking.Cost;
                        break;
                    }
            }

            return toPay;
        }
    }
}
