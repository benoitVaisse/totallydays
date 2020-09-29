using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class BookingRepository
    {
        private readonly TotallydaysContext _context;

        public BookingRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public async Task<Booking> Create(Booking Booking)
        {
            this._context.Bookings.Add(Booking);
            await this._context.SaveChangesAsync();
            return Booking;
        }

        public async Task<Booking>Update(Booking Booking)
        {
            this._context.Bookings.Update(Booking);
            await this._context.SaveChangesAsync();
            return Booking;
        }

        public async Task<Booking> Find(int id)
        {
            return await this._context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> FindByHosting(Hosting Hosting)
        {
            return await this._context.Bookings.Where(b => b.Hosting == Hosting).ToListAsync();
        }

        public async Task<List<Booking>> GetMyBooking(AppUser User)
        {
            var Bookings = await this._context.Bookings.Where(b => b.User == User).ToListAsync();
            return Bookings;
        }
    }
}
