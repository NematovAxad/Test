using MainInfrastructures.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApi
{
    public class WebsitePingService :IHostedService
    {
        private Timer _timer;
        IPingService _pingService;
        public WebsitePingService(IPingService pingService)
        {
            _pingService = pingService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(_pingService.CheckPing, null, TimeSpan.Zero, TimeSpan.FromMinutes(40));
            return Task.CompletedTask;
        }

        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
