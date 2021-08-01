using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            if (roleMgr.Roles.Any())
            {
                var regularUser = new IdentityRole("RegularUser");
                var coworkingAdmin = new IdentityRole("CoworkingAdmin");
                roleMgr.CreateAsync(regularUser).GetAwaiter().GetResult();
                roleMgr.CreateAsync(coworkingAdmin).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
