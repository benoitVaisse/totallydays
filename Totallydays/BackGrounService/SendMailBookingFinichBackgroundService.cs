using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;

namespace Totallydays.BackGrounService
{
    /// <summary>
    /// envoie un email au personne dt le séjour viens de finir
    /// </summary>
    public class SendMailBookingFinichBackgroundService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _service;
        private string Schedule = "* 0 8 * * *";
        private ILogger<SendMailBookingFinichBackgroundService> _logger;

        public SendMailBookingFinichBackgroundService(ILogger<SendMailBookingFinichBackgroundService> logger, IServiceProvider service)
        {
            this._logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            this._service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _service.CreateScope();
            BookingRepository BookingRepository = scope.ServiceProvider.GetRequiredService<BookingRepository>();
            SendMailService SendMailService = scope.ServiceProvider.GetRequiredService<SendMailService>();
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await this.SendMailToBookingFinish(BookingRepository, SendMailService);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                // en milliseconde
                await Task.Delay(3600000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task SendMailToBookingFinish(BookingRepository BookingRepository, SendMailService SendMailService)
        {
            IEnumerable<Booking> Bookings = BookingRepository.FindBookingFinish();
            foreach(Booking b in Bookings)
            {
                await SendMailService.SendMailBookingFinish(b);
            }
            Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
        }
    }
}
