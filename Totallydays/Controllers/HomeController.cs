using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers
{
    public class HomeController : MyController
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserRepository userRepo)
        {
            this._userRepository = userRepo;
            _logger = logger;
        }

        /// <summary>
        /// page d'accueil
        /// </summary>
        /// <returns></returns>
        [HttpGet("/", Name ="home")]
        public IActionResult Index()
        {
            FormSearchHostingViewModel model = new FormSearchHostingViewModel();
            ViewData["Title"] = "Bienvenue";
            if (TempData["error"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["error"].ToString());
            }
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
