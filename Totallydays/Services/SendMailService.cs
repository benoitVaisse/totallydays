using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Totallydays.Controllers;
using Totallydays.Models;
using Totallydays.ViewsModel;

namespace Totallydays.Services
{
    /// <summary>
    /// classe qui gere les envoie de mail
    /// </summary>
    public class SendMailService
    {
        private readonly IEmailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly ControllerExtenstionServiceRazor _controllerExtenstionServiceRazor;

        public SendMailService(IEmailService mailService, IConfiguration configuration, ControllerExtenstionServiceRazor ControllerExtenstionServiceRazor)
        {
            this._mailService = mailService;
            this._configuration = configuration;
            this._controllerExtenstionServiceRazor = ControllerExtenstionServiceRazor;
        }

        /// <summary>
        /// envoie un mail de verification du compte
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        public async Task sendVeridyEmail(AppUser User, string Link)
        {
            await this._mailService.SendAsync(User.Email, "Email Verification", $"<a href=\"{Link}\">click to verify </a>", true);
        }

        public async Task SendEmailNewHosting(Hosting Hosting, Controller Controller)
        {
            string BaseUrl = Controller.Url.RouteUrl("home", new { }, Controller.Request.Scheme, Controller.Request.Host.ToString());
            SendMailVariable variable = new SendMailVariable()
            {
                BaseUrl = BaseUrl,
                Hosting = Hosting
            };
            string view = await ControllerExtenstionService.RenderViewToStringAsync(Controller, "~/Views/Email/SendNewHosting.cshtml", variable);
            await this._mailService.SendAsync(this._configuration["email:admin"], "Nouvelle Hébergemment", view, true);
        }

        public async Task SendMailBookingFinish(Booking b)
        {
            string view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailBookingFinish.cshtml", b);
            await this._mailService.SendAsync(b.User.Email, "Faite un commentaire", view, true);
        }

        public async Task SendEmailForgotPassword(AppUser User, string Url)
        {
            ForgotPasswordEmailViewModel model = new ForgotPasswordEmailViewModel()
            {
                User = User,
                Url = Url
            };
            string view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendEmailForgotPassword.cshtml", model);
            this._mailService.SendAsync(User.Email, "Reset de password", view, true);
        }
    }
}
