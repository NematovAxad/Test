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
        private readonly IRepository<SiteFailsTable, int> _siteFails;
        private IDataContext _db;

        public PingService(IRepository<Organizations, int> org, IRepository<WebSiteAvailability, int> webSiteAvailability, IRepository<Deadline, int> deadline, IDataContext db, IRepository<SiteFailsTable, int> siteFails)
        {
            _org = org;
            _webSiteAvailability = webSiteAvailability;
            _deadline = deadline;
            _db = db;
            _siteFails = siteFails;
        }
        public bool Ping(string website)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                string uri = website;
                uri = uri.Replace("www.", string.Empty);
                uri = uri.Replace("https://", string.Empty);
                uri = uri.Replace("http://", string.Empty);
                uri = uri.Replace("/", string.Empty);
                pinger = new Ping();
                PingReply reply = pinger.Send(uri);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException exc)
            {
                return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }
        public void CheckPing(object obj)
        {

            Console.WriteLine("STARTED");
            List<SiteFailsTable> webSiteFailsList = new List<SiteFailsTable>();
            List<WebSiteAvailability> addModelList = new List<WebSiteAvailability>();
            List<WebSiteAvailability> updateModelList = new List<WebSiteAvailability>();
            List<Organizations> OrgList = new List<Organizations>();
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var organizations = _org.GetAll().ToList();
            if (organizations.Count() == 0)
                throw ErrorStates.NotFound("org");
            var webSite = _webSiteAvailability.Find(w => w.DeadlineId == deadline.Id).ToList();
            foreach (var o in organizations)
            {
                bool pingCheck = Ping(o.WebSite);
                var ws = webSite.Where(w => w.OrganizationId == o.Id).FirstOrDefault();
                HttpWebResponse response = null;
                try
                {
                    string uri = o.WebSite.Replace("www.", string.Empty);
                    WebRequest request = System.Net.WebRequest.Create(o.WebSite);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    if(pingCheck == false)
                    {
                        OrgList.Add(o);
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
                            addModelList.Add(addModel);
                        }
                        if (ws != null)
                        {
                            ws.FailedPing = ws.FailedPing + 1;
                            ws.Website = o.WebSite;
                            updateModelList.Add(ws);
                        }
                    }
                    if(pingCheck == true)
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
                            addModelList.Add(addModel);
                        }
                        if (ws != null)
                        {
                            ws.SuccessfulPing = ws.SuccessfulPing + 1;
                            ws.Website = o.WebSite;
                            updateModelList.Add(ws);
                        }
                    }
                }
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK || pingCheck == true)
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
                            addModelList.Add(addModel);
                        }
                        if (ws != null)
                        {
                            ws.SuccessfulPing = ws.SuccessfulPing + 1;
                            ws.Website = o.WebSite;
                            updateModelList.Add(ws);
                        }
                    }
                    if(response.StatusCode != HttpStatusCode.OK && pingCheck != true)
                    {
                        OrgList.Add(o);
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
                            addModelList.Add(addModel);
                        }
                        if (ws != null)
                        {
                            ws.FailedPing = ws.FailedPing + 1;
                            ws.Website = o.WebSite;
                            updateModelList.Add(ws);
                        }
                    }
                }
            }
            if(addModelList.Count()>0)
            {
                _webSiteAvailability.AddRange(addModelList);
            }
            if(updateModelList.Count()>0)
            {
                _db.Context.Set<WebSiteAvailability>().UpdateRange(updateModelList);
                _db.Context.SaveChanges();
            }
            if(OrgList.Count()>0)
            {
                foreach (Organizations o in OrgList)
                {
                    SiteFailsTable fail = new SiteFailsTable()
                    {
                        OrganizationId = o.Id,
                        DeadlineId = deadline.Id,
                        Website = o.WebSite,
                        FailedTime = DateTime.Now
                    };
                    webSiteFailsList.Add(fail);
                }
                _siteFails.AddRange(webSiteFailsList);
                Console.WriteLine("WRITTEN FAILS");
            }
        }
    }
}
