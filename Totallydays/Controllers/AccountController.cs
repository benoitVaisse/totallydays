using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Totallydays.Models;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _host;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SendMailService _mailService;

        public AccountController(UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager, IWebHostEnvironment host, RoleManager<AppRole> RoleManager, SendMailService mailService)
        {
            this._userManager = UserManager;
            this._signInManager = SignInManager;
            this._host = host;
            this._roleManager = RoleManager;
            this._mailService = mailService;
        }

        /// <summary>
        /// route pour creer de base les 2 role principal
        /// </summary>
        /// <returns></returns>
        [HttpGet("CREATEROLE", Name ="account_create_role")]
        public async Task<IActionResult> CreateRoleBase()
        {
            AppRole RoleAdmin = new AppRole()
            {
                Name = "admin"
            };
            AppRole RoleUser = new AppRole()
            {
                Name = "user"
            };

            var ResultAdmin = await this._roleManager.CreateAsync(RoleAdmin);
            var ResultUser = await this._roleManager.CreateAsync(RoleUser);

            return RedirectToRoute("home");
        }

        /// <summary>
        /// route pour aller sur le formulaire d'inscription
        /// </summary>
        /// <returns></returns>
        [HttpGet("register", Name = "account_register")]
        public IActionResult Register()
        {
            FormRegisterViewModel model = new FormRegisterViewModel();
            return View(model);
        }

        /// <summary>
        /// route qui recois le formulaire d'inscription et le traite pour creer un nouvelle utilisateur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resgiter-post", Name = "account_register_post")]
        public async Task<IActionResult> Register(FormRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser User = new AppUser()
                {
                    UserName = model.Firstname + '_' + model.Lastname,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname
                };

                var result = await this._userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(User, "user");
                    var token = await this._userManager.GenerateEmailConfirmationTokenAsync(User);
                    var url = Url.RouteUrl("email_verify_account", new { id = User.Id, token = token }, Request.Scheme, Request.Host.ToString());

                    await this._mailService.sendVeridyEmail(User, url);
                    return RedirectToRoute("email_verify_view_account");
                }
            }
            return View(model);
        }

        /// <summary>
        /// fonction qui affiche la page qui dis de consulter ses emails
        /// </summary>
        /// <returns></returns>
        [HttpGet("verification", Name = "email_verify_view_account")]
        public IActionResult EmailVerifyView()
        {
            return View();
        }


        /// <summary>
        /// fonction qui verifie l'email grace au token 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("verification/email", Name = "email_verify_account")]
        public async Task<IActionResult> EmailVerify(string id, string token)
        {
            var User = await  this._userManager.FindByIdAsync(id);
            if (User == null) return BadRequest();
            var result = await this._userManager.ConfirmEmailAsync(User, token);
            if (result.Succeeded)
            {
                return RedirectToRoute("home");
            }

            return BadRequest();
        }
    }
}