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