using CoworkingService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoworkingService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<UserInCoworking> UsersInCoworkings { get; set; }
        public DbSet<Coworking> Coworkings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomOccupied> RoomOccupieds { get; set; }
        public DbSet<Picture> Pictures { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserInCoworking>().HasKey(o => new { o.UserId, o.CoworkingId });
        }
    }
}
