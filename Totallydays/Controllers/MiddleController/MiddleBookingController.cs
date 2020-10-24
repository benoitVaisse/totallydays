using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers.MiddleController
{
    [Route("my-account")]
    [Authorize]
    public class MiddleBookingController : MiddleController
    {
        private readonly BookingRepository _bookinRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly CommentService _commentService;

        public MiddleBookingController(
            BookingRepository bookingRepo,
            UserManager<AppUser> userManager,
            CommentService commentService
            )
        {
            this._bookinRepository = bookingRepo;
            this._userManager = userManager;
            this._commentService = commentService;
        }
        [HttpGet("mes-reservations", Name ="my_bookings")]
        public async Task<IActionResult> MyBookings()
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            this.ViewBag.futurBooking = await this._bookinRepository.GetMyFuturBooking(User);
            this.ViewBag.PassedBooking = await this._bookinRepository.GetMyBookingPassed(User);
            this.ViewBag.CancelledBooking = await this._bookinRepository.GetMyBookingCancelled(User);

            this.ViewBag.formComment = new FormCommentViewModel();

            return View();
        }


        [HttpPost("mes-reservations/commentaire/submit", Name = "my_bookings_post_comment")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(FormCommentViewModel model)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            if (ModelState.IsValid)
            {
                Booking Booking = await this._bookinRepository.Find(model.BookingId);
                // si la reservation n'existe pas ou si la réservation ne m'appartient pas 
                if(Booking == null || Booking.User != User)
                {
                    this._errorMessage.Add("Un Problème est survenue lors de l'envoie du commentaire");
                    this._ajaxFlashessage.Add("error", this._errorMessage);

                    return Json(new { status = "error", messages = this._ajaxFlashessage });
                }

                await this._commentService.Create(model, User);
                this._successMessage.Add("Le commentaire a bien été envoyé.");
                this._ajaxFlashessage.Add("success", this._successMessage);
                return Json(new { status = "success", message = this._ajaxFlashessage });
            }

            this.SetErroMessageAjax();
            this._ajaxFlashessage.Add("error", this._errorMessage);

            return Json(new { status="error", messages = this._ajaxFlashessage });
        }
    }
}