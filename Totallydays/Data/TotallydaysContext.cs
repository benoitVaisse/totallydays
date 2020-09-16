using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;

namespace Totallydays.Data
{
    public class TotallydaysContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public TotallydaysContext(DbContextOptions<TotallydaysContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Hosting> Hostings { get; set; }
        public DbSet<Hosting_type> Hosting_Types { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Equipment_type> Equipment_types { get; set; }
        public DbSet<Bedroom> Bedrooms { get; set; }
        public DbSet<Bedroom_Bed> Bedroom_Beds { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Unavailable_date> Unavailable_dates { get; set; }
         
        public DbSet<Hosting_Equipment> Hosting_Equipment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User_emmiter)
                .WithMany(u => u.Comments_emmit);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User_receiver)
                .WithMany(u => u.Comments_receive);

            modelBuilder.Entity<Hosting_Equipment>()
                .HasKey(he => new { he.EquipmentEquipment_id , he.HostingHosting_id });

            modelBuilder.Entity<Hosting>()
                .HasOne(h => h.User)
                .WithMany(u => u.Hostings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hosting>()
                .HasIndex(h => h.Title)
                .IsUnique();

        }

    }
}
