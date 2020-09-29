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
using Totallydays.ViewsModel;

namespace Totallydays.Controllers.MiddleController
{
    [Route("mon-compte")]
    [Authorize]
    public class MiddleUnavailableDateController : MyController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UnavailableDateRepository _unDateRepo;
        private readonly HostingRepository _hostingRepo;

        public MiddleUnavailableDateController(
            UserManager<AppUser> usermanager,
            UnavailableDateRepository unavailableDateRepo,
            HostingRepository hostingRepo
            )
        {
            this._userManager = usermanager;
            this._unDateRepo = unavailableDateRepo;
            this._hostingRepo = hostingRepo;
        }

        [HttpPost("date-indisponible", Name = "make_unavailable_date")]
        public async Task<IActionResult> MakeUnavailableDate(FormUnavailableDateViewModel model)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepo.Find(model.hosting_id);
            List<string> message = new List<string>();

            if (Hosting == null)
            {
                message.Add("impossible de trouver l'hébergement souhaité");
                TempData["error"] = message;
                return RedirectToRoute("my_hostings");
            }

            if (Hosting.User != User)
            {
                message.Add("Error lors de l'envoie des dates d'indisponibilité");
                TempData["error"] = message;
                return RedirectToRoute("hosting_view", new { slug= Hosting.Slug });
            }

            if (ModelState.IsValid)
            {
                Unavailable_date date = new Unavailable_date()
                {
                    Hosting = Hosting,
                    Start_date = model.Start_date,
                    End_date = model.End_date
                };
                await this._unDateRepo.Create(date);
                message.Add("Les dates d'indisponibilité on bien étais enregistré");
                TempData["success"] = message;
                return RedirectToRoute("hosting_view", new { slug = Hosting.Slug });
            }

            foreach (ModelStateEntry modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    message.Add(error.ErrorMessage);
                }
            }
           
            TempData["error"] = message;
            return RedirectToRoute("hosting_view", new { slug = Hosting.Slug });
        }
    }
}