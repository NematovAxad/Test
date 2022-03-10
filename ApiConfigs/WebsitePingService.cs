using Domain.Models;
using Domain.Models.Organization;
using Domain.States;
using JohaRepository;
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
        private readonly IRepository<Organizations, int> _org;
        private readonly IRepository<WebSiteAvailability, int> _webSiteAvailability;
        private readonly IRepository<Deadline, int> _deadline;
        private Timer _timer;
        
        public WebsitePingService(IRepository<Organizations, int> org, IRepository<WebSiteAvailability, int> webSiteAvailability, IRepository<Deadline, int> deadline)
        {
            _org = org;
            _webSiteAvailability = webSiteAvailability;
            _deadline = deadline;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckPing, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private void CheckPing(object state)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var organizations = _org.GetAll().ToList();
            if (organizations.Count() == 0)
                throw ErrorStates.NotFound("org");
            foreach(var o in organizations)
            {
                var ws = _webSiteAvailability.Find(w => w.OrganizationId == o.Id && w.DeadlineId == deadline.Id).FirstOrDefault();
                Ping p = new Ping();
                PingReply r;
                r = p.Send(o.WebSite);
                if(r.Status == IPStatus.Success)
                {
                    if (ws == null)
                    {
                        WebSiteAvailability addModel = new WebSiteAvailability()
                        {
                            OrganizationId = o.Id,
                            DeadlineId = deadline.Id,
                            Website = o.WebSite,
                            SuccessfulPing = 1,
                            FailedPing = 0
                        };
                        _webSiteAvailability.Add(addModel);
                    }
                    if(ws != null)
                    {
                        ws.SuccessfulPing = ws.SuccessfulPing + 1;
                        _webSiteAvailability.Update(ws);
                    }
                }
                if(r.Status != IPStatus.Success)
                {
                    if (ws == null)
                    {
                        WebSiteAvailability addModel = new WebSiteAvailability()
                        {
                            OrganizationId = o.Id,
                            DeadlineId = deadline.Id,
                            Website = o.WebSite,
                            SuccessfulPing = 0,
                            FailedPing = 1
                        };
                        _webSiteAvailability.Add(addModel);
                    }
                    if (ws != null)
                    {
                        ws.FailedPing = ws.FailedPing + 1;
                        _webSiteAvailability.Update(ws);
                    }
                }
            }    

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
