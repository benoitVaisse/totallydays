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


        /// <summary>
        /// envoie un maila l'admin pour avertir qu'un hébergement a été créé
        /// </summary>
        /// <param name="Hosting"></param>
        /// <param name="Controller"></param>
        /// <returns></returns>
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

        /// <summary>
        /// envoie un mail le lendemin de la fin du séjour pour proposé de de laisser un commentaire
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public async Task SendMailBookingFinish(Booking b)
        {
            string view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailBookingFinish.cshtml", b);
            await this._mailService.SendAsync(b.User.Email, "Faite un commentaire", view, true);
        }


        /// <summary>
        /// envoie un mail ppour dire que la réservation a été validé ou réfusé
        /// </summary>
        /// <param name="model"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public async Task SendMailChangeStatusBooking(FormHostingBookingValidation model, Booking b)
        {
            FormHostingBookingValidationModel variable = new FormHostingBookingValidationModel()
            {
                Booking = b,
                Comment = model.Comment
            };
            string view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailBookingStatusChange.cshtml", variable);
            await this._mailService.SendAsync(b.User.Email, "Status Réservation", view, true);
        }

        /// <summary>
        /// envoie un mail pour avertir un utilisateur qui'il a des reservation en attente sur ses hébergements
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendMailUserHostingBookingPending(AppUser user)
        {
            try
            {
                string view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailUserHostingBookingPending.cshtml", user);
                await this._mailService.SendAsync(user.Email, "Réservation en attente", view, true);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        /// envoie de mail la veuille d'un séjour a lhébergeur et au client pour les avertir que le séjour commence le lendemain
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public async Task SendMailToBookingStarting(Booking b)
        {
            try
            {
                string viewBookeur = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailToBookingStartingBookeur.cshtml", b);
                string viewOwner = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Email/SendMailToBookingStartingOwner.cshtml", b);
                this._mailService.SendAsync(b.User.Email, "Votre séjour vous attend", viewBookeur, true);
                this._mailService.SendAsync(b.Hosting.User.Email, "Une réservation commence demain", viewOwner, true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        /// envoie de mail pour le mot de passe oublé
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
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
