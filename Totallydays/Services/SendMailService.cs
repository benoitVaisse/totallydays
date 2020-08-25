using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;

namespace Totallydays.Services
{
    /// <summary>
    /// classe qui gere les envoie de mail
    /// </summary>
    public class SendMailService
    {
        private readonly IEmailService _mailService;

        public SendMailService(IEmailService mailService)
        {
            this._mailService = mailService;
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
    }
}
