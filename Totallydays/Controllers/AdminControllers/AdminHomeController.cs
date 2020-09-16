using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminHomeController : AdminController
    {
        [HttpGet("", Name = "admin_home")]
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}