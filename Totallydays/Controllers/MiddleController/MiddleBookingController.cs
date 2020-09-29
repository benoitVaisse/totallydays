using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Controllers.MiddleController
{
    [Route("my-account")]
    [Authorize]
    public class MiddleBookingController : MyController
    {
        private readonly BookingRepository _bookinRepository;
        private readonly UserManager<AppUser> _userManager;

        public MiddleBookingController(
            BookingRepository bookingRepo,
            UserManager<AppUser> userManager
            )
        {
            this._bookinRepository = bookingRepo;
            this._userManager = userManager;
        }
        [HttpGet("mes-reservations", Name ="my_bookings")]
        public async Task<IActionResult> MyBookings()
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            List<Booking> Bookings = await this._bookinRepository.GetMyBooking(User);
            return View(Bookings);
        }
    }
}