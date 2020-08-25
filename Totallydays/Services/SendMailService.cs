using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;

namespace Totallydays.Services
{
    public class SendMailService
    {
        private readonly IEmailService _mailService;

        public SendMailService(IEmailService mailService)
        {
            this._mailService = mailService;
        }

        public async Task sendVeridyEmail(AppUser User, string Link)
        {
            await this._mailService.SendAsync(User.Email, "Email Verification", $"<a href=\"{Link}\">click to verify </a>", true);
        }
    }
}
