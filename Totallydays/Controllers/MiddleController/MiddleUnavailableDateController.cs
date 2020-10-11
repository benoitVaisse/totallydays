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
    [Route("mon-compte")]
    [Authorize]
    public class MiddleUnavailableDateController : MiddleController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UnavailableDateRepository _unDateRepo;
        private readonly HostingRepository _hostingRepo;
        private readonly ControllerExtenstionServiceRazor _controllerExtenstionServiceRazor;

        public MiddleUnavailableDateController(
            UserManager<AppUser> usermanager,
            UnavailableDateRepository unavailableDateRepo,
            HostingRepository hostingRepo,
            ControllerExtenstionServiceRazor ControllerExtenstionServiceRazor
            )
        {
            this._userManager = usermanager;
            this._unDateRepo = unavailableDateRepo;
            this._hostingRepo = hostingRepo;
            this._controllerExtenstionServiceRazor = ControllerExtenstionServiceRazor;
        }

        /// <summary>
        /// enregistrement des date d'indisponibilités pour un hébergement
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("date-indisponible", Name = "make_unavailable_date")]
        public async Task<IActionResult> MakeUnavailableDate(FormUnavailableDateViewModel model)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepo.Find(model.hosting_id);
            List<string> messages = new List<string>();

            if (Hosting == null)
                return this.NotFindObject("my_hostings", messages, "impossible de trouver l'hébergement souhaité");

            var NotHostingToUser = this.NotHostingToUser(User, Hosting, messages, "Error lors de l'envoie des dates d'indisponibilité");
            if (NotHostingToUser != null)
                return NotHostingToUser;

            if (ModelState.IsValid)
            {
                Unavailable_date date = new Unavailable_date()
                {
                    Hosting = Hosting,
                    Start_date = model.Start_date,
                    End_date = model.End_date
                };
                await this._unDateRepo.Create(date);
                messages.Add("Les dates d'indisponibilité on bien étais enregistré");
                TempData["success"] = messages;
                return RedirectToRoute("hosting_view", new { slug = Hosting.Slug });
            }

            foreach (ModelStateEntry modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    messages.Add(error.ErrorMessage);
                }
            }
           
            TempData["error"] = messages;
            return RedirectToRoute("hosting_view", new { slug = Hosting.Slug });
        }

        [HttpPost("hosting/{slug}/date-indisponible/{id:int}/delete", Name = "middle_delete_unavailable_date")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUnavailableDate(string slug, int id)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            Unavailable_date Unavailable_Date = await this._unDateRepo.FindById(id);
            Hosting Hosting = this._hostingRepo.FindBySlug(slug);
            if (Hosting == null || Unavailable_Date == null)
                return this.NotFindObjectAjax("Erreur lors de la suppresion de ces dates"); ;

            var NotHostingToUserAjax = this.NotHostingToUserAjax(User, Hosting, "Erreur lors de la suppresion de ces dates");
            if (NotHostingToUserAjax != null)
                return NotHostingToUserAjax;

            Hosting = this._hostingRepo.FindBySlug(slug);
            Unavailable_Date = await this._unDateRepo.Delete(Unavailable_Date);
            IEnumerable<Unavailable_date> dates = await this._unDateRepo.FindAll();
            return Json(new { 
                status = "success",
                view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Modal/_partial/_myUnavailableDateHostingModalTable.cshtml", Hosting.GetMyNextUnavailableDate()),
                unvavalibledate= Hosting.getUnavailableDaysToArray()
            });
            
        }
    }
}