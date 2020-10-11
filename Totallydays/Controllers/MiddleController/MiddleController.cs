using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;

namespace Totallydays.Controllers.MiddleController
{
    public class MiddleController : MyController
    {
        

        /// <summary>
        /// retour sur la page d'un hebergement 
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Hosting"></param>
        /// <param name="messages"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult NotHostingToUser(AppUser User, Hosting Hosting, List<string> messages, string message)
        {
            if (Hosting.User != User)
            {
                messages.Add(message);
                TempData["error"] = messages;
                return RedirectToRoute("hosting_view", new { slug = Hosting.Slug });
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// on verifie si l'hébergement appartient bien a l'utilisateur
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Hosting"></param>
        /// <param name="messages"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult NotHostingToUserAjax(AppUser User, Hosting Hosting, string message)
        {
            if (Hosting.User != User)
            {
                this._errorMessage.Add(message);
                this._ajaxFlashessage.Add("error", this._errorMessage);
                return Json(new { status = "error", messages = this._ajaxFlashessage });
            }
            else
            {
                return null;
            }
        }


        

        public void AddAjaxErrorMessage()
        {

        }
    }
}
