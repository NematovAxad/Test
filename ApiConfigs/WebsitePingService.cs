using Domain.Models;
using Domain.Models.Organization;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiConfigs
{
    public class WebsitePingService : IHostedService , IDisposable
    {
        private Timer _timer;
        IPingService _pingService;
        public WebsitePingService(IPingService pingService)
        {
            _pingService = pingService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(_pingService.CheckPing, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
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
