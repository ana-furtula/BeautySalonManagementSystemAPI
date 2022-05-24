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
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<ScheduledAppointment> ScheduledAppointments { get; set; }
        public DbSet<NonWorkingDay> NonWorkingDays { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
               .HasData(
                   new User() { Id = 1, FirstName = "admin", LastName = "admin", Email = "admin@gmail.com", Password = "admin", Role = Role.ADMIN }
               );
            builder.Entity<Treatment>()
               .HasData(
                   new Treatment() { Id = 1, Name = "Manikir", Description = "Neki opis", Price = 1200 },
                   new Treatment() { Id = 2, Name = "Pedikir", Description = "Neki opis", Price = 1500 }
               );
            builder.Entity<WorkingHour>()
               .HasData(
                   new WorkingHour() { Id = 1, Time = "8:30" },
                  new WorkingHour() { Id = 2, Time = "9:30" },
                  new WorkingHour() { Id = 3, Time = "10:30" },
                  new WorkingHour() { Id = 4, Time = "11:00" },
                  new WorkingHour() { Id = 5, Time = "12:00" },
                  new WorkingHour() { Id = 6, Time = "13:30" },
                  new WorkingHour() { Id = 7, Time = "14:00" },
                  new WorkingHour() { Id = 8, Time = "15:00" }
               );

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= BeautySalon_App_DB;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }

    }
}
