using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        public ScheduledProcessor(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            var options = new CrontabSchedule.ParseOptions
            {
                IncludingSeconds = true
            };
            _schedule = CrontabSchedule.Parse(Schedule, options);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected abstract string Schedule { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process().ConfigureAwait(false);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }

                await Task.Delay(5000, stoppingToken).ConfigureAwait(false); //5 seconds delay
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}
