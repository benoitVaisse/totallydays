using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers.FrontController
{
    public class HostingController : MyController
    {
        private readonly HostingRepository _hostingRepository;
        private readonly UserManager<AppUser> _usermanager;
        private readonly HostingTypeRepository _hostingTypeRepo;
        private readonly EquipmentRepository _equipementRepository;
        private readonly HostingService _hostingService;
        private readonly ImageRepository _imageRepository;
        private readonly UploadService _uploadService;
        private readonly BookingRepository _bookingRepository;
        private readonly BookingService _bookingService;

        public HostingController(HostingRepository hostrepo,
            UserManager<AppUser> usermanager,
            HostingTypeRepository hostingTypeRepo,
            EquipmentRepository equipementRepository,
            HostingService hostingService,
            ImageRepository imagerepo,
            UploadService uploadServ,
            BookingRepository bookingRepo,
            BookingService bookingServ
         )
        {
            this._hostingRepository = hostrepo;
            this._usermanager = usermanager;
            this._hostingTypeRepo = hostingTypeRepo;
            this._equipementRepository = equipementRepository;
            this._hostingService = hostingService;
            this._imageRepository = imagerepo;
            this._uploadService = uploadServ;
            this._bookingRepository = bookingRepo;
            this._bookingService = bookingServ;
        }

        /// <summary>
        /// To see a hosting
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("hebergement/{slug}", Name ="hosting_view")]
        public IActionResult SeeHosting(string slug)
        {
            this.setFlash();
            
            Hosting Hosting = this._hostingRepository.FindBySlug(slug);
            if (Hosting == null)
                return BadRequest();

            this.ViewBag.hosting = Hosting;
            this.ViewBag.unavailable_date = new FormUnavailableDateViewModel()
            {
                hosting_id = Hosting.Hosting_id
            };
            this.ViewBag.bookingModel = new FormBookingViewModel()
            {
                hosting_id = Hosting.Hosting_id
            };
            return View(Hosting);
        }

        [HttpPost("hebergement/{slug}", Name ="hosting_booking_post")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeeHosting(string slug, FormBookingViewModel model)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.FindBySlug(slug);
            if (Hosting == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                Booking booking = this._bookingService.BindBooking(model, User, Hosting);
                if (booking.IsBookingableDate())
                {
                    await this._bookingRepository.Create(booking);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ces Dates sont Indisponible");
                }
                
            }

            this.ViewBag.hosting = Hosting;
            this.ViewBag.bookingModel = new FormBookingViewModel()
            {
                hosting_id = Hosting.Hosting_id
            };
            return View(Hosting);
        }

        [HttpGet("hosting", Name ="hostings-search")]
        public IActionResult GetHostingSearch(string City)
        {
            IEnumerable<Hosting> Hostings = this._hostingRepository.FindAll();
            return View(Hostings);
        }

        public void setFlash()
        {
            this.ViewBag.error = null;
            if (TempData["error"] != null)
            {
                this.ViewBag.flashError = TempData["error"];
            }

            ViewBag.success = null;
            if (TempData["success"] != null)
            {
                this.ViewBag.flashSuccess = TempData["success"];
            }
        }
    }
}