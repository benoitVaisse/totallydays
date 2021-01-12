using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly HostingTypeRepository _hostingTypeRepo;
        private readonly EquipmentRepository _equipementRepository;
        private readonly HostingService _hostingService;
        private readonly ImageRepository _imageRepository;
        private readonly UploadService _uploadService;
        private readonly BookingRepository _bookingRepository;
        private readonly BookingService _bookingService;

        public HostingController(HostingRepository hostrepo,
            UserManager<AppUser> usermanager,
            RoleManager<AppRole> roleManager,
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
            this._roleManager = roleManager;
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
        [Authorize]
        public async Task<IActionResult> SeeHosting(string slug)
        {
            this.setFlash();
            
            Hosting Hosting = this._hostingRepository.FindBySlug(slug);
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            bool isAdmin = await this._usermanager.IsInRoleAsync( User, "admin");
            if (Hosting == null || ((Hosting.Published == false || Hosting.Active == false) && (!isAdmin && Hosting.User != User ) ))
                return NotFound();

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
        public async Task<IActionResult> GetHostingSearch([FromQuery] FormSearchHostingViewModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Hosting> Hostings = await this._hostingRepository.SearchHosting(model);

                return View(Hostings);
            }
            this.TempData["error"] ="Erreur lors de la recherche d'un hébergement";
            return RedirectToRoute("home");
        }


    }
}