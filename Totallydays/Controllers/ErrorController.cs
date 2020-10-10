using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Totallydays.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            // handle different codes or just return the default error view
            return View(code);
        }
    }
}