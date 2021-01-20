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
    public class SendMailBookingBeginBackgroundService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _service;
        private string Schedule = "* 0 8 * * *";
        private ILogger<SendMailBookingBeginBackgroundService> _logger;

        public SendMailBookingBeginBackgroundService(ILogger<SendMailBookingBeginBackgroundService> logger, IServiceProvider service)
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
                    await this.SendMailToBookingStarting(BookingRepository, SendMailService);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                // en milliseconde
                await Task.Delay(1000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task SendMailToBookingStarting(BookingRepository BookingRepository, SendMailService SendMailService)
        {
            IEnumerable<Booking> Bookings = BookingRepository.FindBookingByDayModifier(+1);
            foreach(Booking b in Bookings)
            {
                await SendMailService.SendMailToBookingStarting(b);
            }
        }
    }
}
