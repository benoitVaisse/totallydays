using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.ViewsModel;

namespace Totallydays.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService(BookingRepository bookingRepo)
        {
            this._bookingRepository = bookingRepo;
        }

        public Booking BindBooking(FormBookingViewModel model, AppUser User , Hosting Hosting)
        {
            Booking booking = new Booking()
            {
                Comment = model.Comment,
                Start_date = model.Start_date,
                End_date = model.End_date,
                User = User,
                Hosting = Hosting,
                Created_at = DateTime.Now,
                Validated = 0
            };

            booking.Amount = this.CalculateAmount(booking, Hosting);

            return booking;
        }

        public float CalculateAmount(Booking booking, Hosting Hosting)
        {
            return (float)((booking.End_date - booking.Start_date).TotalDays) * Hosting.Price;
        }
    }
}
