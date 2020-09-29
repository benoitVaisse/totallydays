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

        public SendMailService(IEmailService mailService, IConfiguration configuration)
        {
            this._mailService = mailService;
            this._configuration = configuration;
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
    }
}
