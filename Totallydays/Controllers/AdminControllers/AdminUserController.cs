using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminUserController : AdminController
    {
        private readonly UserRepository _userRepository;

        public AdminUserController(UserRepository userRepo)
        {
            this._userRepository = userRepo;
        }

        [HttpGet("users", Name = "admin_Users")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AllUser()
        {
            IEnumerable<AppUser> Users =  await this._userRepository.FindAll();
            return View(Users);
        }
    }
}