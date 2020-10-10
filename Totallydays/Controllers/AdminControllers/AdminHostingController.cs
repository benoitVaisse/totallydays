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

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminHostingController : AdminController
    {
        private readonly HostingRepository _hostingRepository;
        private readonly HostingService _hostingService;
        private readonly UserManager<AppUser> _usermanager;

        public AdminHostingController(
            HostingRepository hostingRepo,
            HostingService HostingService,
            UserManager<AppUser> usermanager
            )
        {
            this._hostingRepository = hostingRepo;
            this._hostingService = HostingService;
            this._usermanager = usermanager;
        }
        
        [HttpGet("hosting", Name ="admin_hostings")]
        public IActionResult AllHosting()
        {
            IEnumerable<Hosting> Hostings = this._hostingRepository.FindAll();
            return View(Hostings);
        }

        [HttpPost("hosting/{id:int}/active",Name = "admin_hosting_active")]
        public async Task<IActionResult> ActiveHosting(int id, bool active)
        {
            this._successMessage.Clear();
            this._ajaxFlashessage.Clear();
            Hosting Hosting = this._hostingRepository.Find(id);
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            bool isAdmin = await this._usermanager.IsInRoleAsync(User, "admin");

            if (Hosting == null || !isAdmin)
                return NotFound();

            Hosting = this._hostingService.setActive(Hosting, active);
            this._successMessage.Add($"l'hébergement {Hosting.Title} a bien été {(Hosting.Active == true ? "activé": "désactivé")}");
            this._ajaxFlashessage.Add("success", this._successMessage);
            return Json(new { status="success", message = this._ajaxFlashessage });
        }
    }
}