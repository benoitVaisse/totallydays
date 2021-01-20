using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;

namespace Totallydays.BackGrounService
{
    /// <summary>
    /// envoie un mail pour chaque utilisateur qui a des reservation en attente sur ses hébergement
    /// </summary>
    public class SendMailHostingBookingPendingBackgroundService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _service;
        private string Schedule = "* 0 8 * * *";
        private ILogger<SendMailHostingBookingPendingBackgroundService> _logger;

        public SendMailHostingBookingPendingBackgroundService(ILogger<SendMailHostingBookingPendingBackgroundService> logger, IServiceProvider service)
        {
            this._logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            this._service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _service.CreateScope();
            UserRepository UserRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
            SendMailService SendMailService = scope.ServiceProvider.GetRequiredService<SendMailService>();
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await this.SendMailToBookingFinish(UserRepository, SendMailService);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                // en millisenconde
                // 1h
                await Task.Delay(3600000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task SendMailToBookingFinish(UserRepository UserRepository, SendMailService SendMailService)
        {
            IEnumerable<AppUser> Users = await UserRepository.GetUserWithHosting();
            foreach (AppUser user in Users)
            {
                if (user.GetNumberHostingWithBookingPending() > 0)
                {
                    await SendMailService.SendMailUserHostingBookingPending(user);
                }
            }
        }
    }
}

