using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public abstract class BackgroundService : IHostedService
    {
        private readonly CancellationTokenSource _stoppingCts =
            new CancellationTokenSource();

        private Task _executingTask;

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            if (_executingTask.IsCompleted) return _executingTask;
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null) return;

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken)).ConfigureAwait(false);
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await Process().ConfigureAwait(false);

                await Task.Delay(5000, stoppingToken).ConfigureAwait(false);
            } while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task Process();
    }
}
