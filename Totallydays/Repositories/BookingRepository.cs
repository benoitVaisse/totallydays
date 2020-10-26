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

        public async Task<List<Booking>> GetMyFuturBooking(AppUser User)
        {
            return await this._context.Bookings.Where(b => b.User == User).Where(b=>b.Validated != Booking.CANCELLED ).Where(b => b.Start_date > DateTime.Now).ToListAsync();
        }
        public async Task<List<Booking>> GetMyBookingPassed(AppUser User)
        {
            return await this._context.Bookings.Where(b => b.User == User).Where(b => b.Validated == Booking.VALIDATED).Where(b => b.End_date < DateTime.Now).ToListAsync();
           
        }
        public async Task<List<Booking>> GetMyBookingCancelled(AppUser User)
        {
            return  await this._context.Bookings.Where(b => b.User == User).Where(b => b.Validated == Booking.CANCELLED).ToListAsync();
            
        }

        /// <summary>
        /// selectionne les reservation qui vienne de ce finir a j+1 de la fin du sejour
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Booking> FindBookingFinish()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime Yesterday = yesterday.Date;
            var query = this._context.Bookings.Where(b => b.End_date == Yesterday).Where(b => b.Rating == null);
            IEnumerable<Booking> Bookings = query.ToList();
            return Bookings;


        }
    }
}
