
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Totallydays.BackGrounService
{
    public class TestBackgroundService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private string Schedule = "*/10 * * * * *";
        private ILogger<TestBackgroundService> _logger;

        public TestBackgroundService(ILogger<TestBackgroundService> logger)
        {
            this._logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
        }
    }
}
