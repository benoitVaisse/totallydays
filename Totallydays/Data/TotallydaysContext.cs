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

    }
}
