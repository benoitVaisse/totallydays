using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Totallydays.Controllers
{
    public class MyController : Controller
    {
        public List<string> _errorMessage = new List<string>();
        public List<string> _successMessage = new List<string>();
        public Dictionary<string, List<string>> _ajaxFlashessage = new Dictionary<string, List<string>>();
        public MyController()
        {
            
        }

        /// <summary>
        /// si l'objet est null on rtour un message flash d'erreur et une redisrection
        /// </summary>
        /// <param name="Object"></param>
        /// <param name="Route"></param>
        /// <param name="messages"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult NotFindObject(string Route, List<string> messages, string message)
        {
            messages.Add(message);
            TempData["error"] = messages;
            return RedirectToRoute(Route);

        }

        /// <summary>
        /// si l'object et null on retour un message flash ajax
        /// </summary>
        /// <param name="Hosting"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult NotFindObjectAjax(string message)
        {
            this._errorMessage.Add(message);
            this._ajaxFlashessage.Add("error", this._errorMessage);
            return Json(new { status = "error", messages = this._ajaxFlashessage });

        }

        /// <summary>
        /// on met dans une variable les erreurs de formulaire pour renvoyer au callback ajax
        /// </summary>
        public void SetErroMessageAjax()
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    this._errorMessage.Add(modelError.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
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