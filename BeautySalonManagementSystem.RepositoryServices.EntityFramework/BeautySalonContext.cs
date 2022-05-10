using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework
{
    public class BeautySalonContext : DbContext
    {
        private readonly IConfiguration config;

        public BeautySalonContext(DbContextOptions<BeautySalonContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Treatment> Treatments{ get; set; }
        public DbSet<ScheduledAppointment> ScheduledAppointments { get; set; }
        public DbSet<NonWorkingDay> NonWorkingDays { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
               .HasData(
                   new User() { Id = 1, FirstName = "admin", LastName = "admin", Email = "admin@gmail.com", Password = "admin", Role = Role.ADMIN }
               );
            builder.Entity<Treatment>()
               .HasData(
                   new Treatment() { Id = 1, Name = "Manikir", Description = "Neki opis", Price = 1200},
                   new Treatment() { Id = 2, Name = "Pedikir", Description = "Neki opis", Price = 1500 }
               );

            base.OnModelCreating(builder);
        }
    }
}
