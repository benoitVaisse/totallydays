using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminHostingController : AdminController
    {
        private readonly HostingRepository _hostingRepository;

        public AdminHostingController(
            HostingRepository hostingRepo
            )
        {
            this._hostingRepository = hostingRepo;
        }
        
        [HttpGet("hosting", Name ="admin_hostings")]
        public IActionResult AllHosting()
        {
            IEnumerable<Hosting> Hostings = this._hostingRepository.FindAll();
            return View(Hostings);
        }
    }
}