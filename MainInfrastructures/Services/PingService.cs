using Domain.Models;
using Domain.Models.Organization;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ApiConfigs
{
    public class PingService:IPingService
    {
        private readonly IRepository<Organizations, int> _org;
        private readonly IRepository<WebSiteAvailability, int> _webSiteAvailability;
        private readonly IRepository<Deadline, int> _deadline;
        private IDataContext _db;

        public PingService(IRepository<Organizations, int> org, IRepository<WebSiteAvailability, int> webSiteAvailability, IRepository<Deadline, int> deadline, IDataContext db)
        {
            _org = org;
            _webSiteAvailability = webSiteAvailability;
            _deadline = deadline;
            _db = db;
        }

        public void CheckPing(object state)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var organizations = _org.GetAll().ToList();
            if (organizations.Count() == 0)
                throw ErrorStates.NotFound("org");
            var webSite = _webSiteAvailability.Find(w => w.DeadlineId == deadline.Id).ToList();
            foreach (var o in organizations)
            {
                var ws = webSite.Where(w => w.OrganizationId == o.Id).FirstOrDefault();
                HttpWebResponse response = null;
                try
                {
                    string uri = o.WebSite.Replace("www.", string.Empty);
                    WebRequest request = System.Net.WebRequest.Create(o.WebSite);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (ws==null)
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
                        if (ws!=null)
                        {
                            ws.SuccessfulPing = ws.SuccessfulPing + 1;
                            ws.Website = o.WebSite;
                            _webSiteAvailability.Update(ws);
                        }
                    }
                    else
                    {
                        if (ws==null)
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
                        if (ws!=null)
                        {
                            ws.FailedPing = ws.FailedPing + 1;
                            ws.Website = o.WebSite;
                            _webSiteAvailability.Update(ws);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ws==null)
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
                    if (ws!=null)
                    {
                        ws.FailedPing = ws.FailedPing + 1;
                        ws.Website = o.WebSite;
                        _webSiteAvailability.Update(ws);
                    }
                }
            }
        }
    }
}
