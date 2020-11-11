using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers
{

    /// <summary>
    /// controller allows to manage login logout and regiter
    /// </summary>
    public class AccountController : MyController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _host;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SendMailService _mailService;
        private readonly TotallydaysContext context;

        public AccountController(UserManager<AppUser> UserManager, 
            SignInManager<AppUser> SignInManager, 
            IWebHostEnvironment host, 
            RoleManager<AppRole> RoleManager, 
            SendMailService mailService,
            TotallydaysContext context)
        {
            this._userManager = UserManager;
            this._signInManager = SignInManager;
            this._host = host;
            this._roleManager = RoleManager;
            this._mailService = mailService;
            this.context = context;
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
                    UserName = model.Email,
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

        /// <summary>
        /// action qui affiche la page de login
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpGet("login", Name = "login_account")]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            FormLoginViewModel model = new FormLoginViewModel()
            {
                ReturnUrl = ReturnUrl,
                ExternalLogings = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        /// <summary>
        /// action pour ce logguer manuellement
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ligin-post", Name = "login_account_post")]
        public async Task<IActionResult> Login(FormLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser User = await this._userManager.FindByEmailAsync(model.Email);
                if(User != null)
                {
                    var PasswordConfirm = await this._userManager.CheckPasswordAsync(User, model.Password);
                    // si l'email n'est pas confirmé
                    if(PasswordConfirm && !User.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Email non confirmé");
                        return View(model);
                    }

                    var result = await this._signInManager.PasswordSignInAsync(User, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToRoute("home");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Email ou password érroné");
            return View(model);
        }


        /// <summary>
        /// action qui nous permet de nous autehtifier avec google ou un autre identifier extern
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost("external-login", Name = "login_extern_account_post")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var ReturnUrl = Url.RouteUrl("login_external_callback_account", new { returnUrl = returnUrl });

            var properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, ReturnUrl);
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// action effectuer apres la login via un provider extern
        /// si on connais deja le user on le log , si on le connais pas on rentre c'est info extern en base
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet("external-login-callback", Name = "login_external_callback_account")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var ReturnUrl = returnUrl ?? Url.RouteUrl("home");

            // on reconstruit notre FormLoginViewModel
            FormLoginViewModel model = new FormLoginViewModel()
            {
                ReturnUrl = ReturnUrl,
                ExternalLogings = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            // on regarde si il y a des erreurs
            if(remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Erreur Provonant de {remoteError}");
            }

            var info = await this._signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                ModelState.AddModelError(string.Empty, $"Erreur Provonant de {remoteError}");
            }

            var signInResult = await this._signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (signInResult.Succeeded)
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                string email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    AppUser User = await this._userManager.FindByEmailAsync(email);
                    if(User == null)
                    {
                        User = new AppUser()
                        {
                            UserName = email,
                            Email = email,
                            Lastname = info.Principal.FindFirstValue(ClaimTypes.Surname),
                            Firstname = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            EmailConfirmed = true
                        };

                         await this._userManager.CreateAsync(User);
                         await this._userManager.AddToRoleAsync(User, "user");


                    }

                    await this._userManager.AddLoginAsync(User, info);
                    await this._signInManager.SignInAsync(User, false);

                    return Redirect(ReturnUrl);
                }
                
            }

            return View("login", model);
        }


        /// <summary>
        /// action pour ce deconnecter
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout", Name ="logout_account")]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToRoute("home");
        }

        [HttpGet("set-new-password", Name = "set-new-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            FormForgotPasswordViewModel model = new FormForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost("set-new-password", Name = "set-new-password-post")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(FormForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser User = await this._userManager.FindByEmailAsync(model.Email);
                if(User != null && await this._userManager.IsEmailConfirmedAsync(User))
                {
                    var token = await this._userManager.GeneratePasswordResetTokenAsync(User);
                    var PasswordLinkToken = Url.RouteUrl("reset_forgot_password", new { Email = User.Email, Token = token }, Request.Scheme, Request.Host.ToString());
                    this._mailService.SendEmailForgotPassword(User, PasswordLinkToken);
                }

                return RedirectToRoute("forgot_password_send_email");
            }
            return View(model);
        }

        [HttpGet("password-oublié", Name= "forgot_password_send_email")]
        public IActionResult ForgotPasswordSendEmail()
        {
            return View();
        }


        [HttpGet("reset_forgot_password", Name = "reset_forgot_password")]
        public async Task<IActionResult> resetForgotPassword(string Email, string Token)
        {
            return View();
        }
    }

}