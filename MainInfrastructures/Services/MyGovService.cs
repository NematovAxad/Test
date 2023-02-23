using Domain;
using Domain.IntegrationLinks;
using Domain.Models;
using Domain.Models.Organization;
using Domain.MonitoringModels.Models;
using Domain.MyGovModels;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Services
{
    public class MyGovService : IMyGovService
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<MygovReports, int> _mygovReports;
        private readonly IRepository<MygovReportsDetail, int> _mygovReportsDetail;
        private readonly IDataContext _db;

        public MyGovService(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<MygovReports, int> mygovReports, IRepository<MygovReportsDetail, int> mygovReportsDetail, IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _mygovReports = mygovReports;
            _mygovReportsDetail = mygovReportsDetail;
            _db = db;
        }

        public async Task<List<MygovReportsDetail>> MygovReportsDetails(int serviceId)
        {
            List<MygovReportsDetail> serviceList = new List<MygovReportsDetail>();

            serviceList = _mygovReportsDetail.Find(r => r.ServiceId == serviceId).ToList();

            return serviceList;
        }

        public async Task<List<MygovReports>> OrgServiceReport(int orgId, int deadlineId)
        {
            List<MygovReports> serviceList = new List<MygovReports>();

            var organization = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (organization == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(o => o.Id == deadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            int part = 0;
            if (deadline.Quarter == Domain.Enums.Quarters.First || deadline.Quarter == Domain.Enums.Quarters.Second)
                part = 1;
            if (deadline.Quarter == Domain.Enums.Quarters.Third || deadline.Quarter == Domain.Enums.Quarters.Fourth)
                part = 2;

            serviceList = _mygovReports.Find(r => r.OrganizationId == orgId && r.Year == deadline.Year && r.Part == part).ToList();
            
            return serviceList;
        }

        public async Task<bool> UpdateMyGovReport(int deadlineId)
        {
            List<MygovReports> serviceList = new List<MygovReports>();

            

            var deadline = _deadline.Find(o => o.Id == deadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
            var orgList = _organization.GetAll();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var byteArray = Encoding.ASCII.GetBytes("elektron.hukumat.loyihalarini.boshqarish.markazi.207322159:QaMZo\\*L.)}Yk&@bjH3Qm~7L3T~\"=:JJEm&hCiBQ");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            int part = 0;
            if (deadline.Quarter == Domain.Enums.Quarters.First || deadline.Quarter == Domain.Enums.Quarters.Second)
                part = 1;
            if (deadline.Quarter == Domain.Enums.Quarters.Third || deadline.Quarter == Domain.Enums.Quarters.Fourth)
                part = 2;

            var url = Links.MyGovServices + "year=" + deadline.Year + "&part=" + part;

            var response = await client.GetAsync(url).ConfigureAwait(false);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var responseResult = JsonConvert.DeserializeObject<List<OrgserviceRecord>>(jsonString).AsEnumerable();
                    

                    foreach (var i in responseResult)
                    {
                        if (serviceList.Any(r => r.ServiceId == i.MyGovService.Id && r.Id == i.MainOrganization.Id))
                        {

                            var item = serviceList.Where(r => r.ServiceId == i.MyGovService.Id && r.Id == i.MainOrganization.Id).FirstOrDefault();

                            item.AllRequests = item.AllRequests + i.Tasks.All;
                            item.LateRequests = item.LateRequests + i.Tasks.Deadline;
                        }
                        else
                        {
                            serviceList.Add(new MygovReports
                            {
                                MygovId = i.MainOrganization.Id,
                                OrganizationId = orgList.Where(o => o.MyGovId == i.MainOrganization.Id).FirstOrDefault() == null ? 0 : orgList.Where(o => o.MyGovId == i.MainOrganization.Id).FirstOrDefault().Id,
                                Name = orgList.Where(o => o.MyGovId == i.MainOrganization.Id).FirstOrDefault() == null ? String.Empty : orgList.Where(o => o.MyGovId == i.MainOrganization.Id).FirstOrDefault().ShortName,
                                ServiceId = i.MyGovService.Id,
                                ServiceName = i.MyGovService.Name,
                                Year = deadline.Year,
                                Part = part,
                                AllRequests = i.Tasks.All,
                                LateRequests = i.Tasks.Deadline
                            });
                        }
                    }
                }
                catch
                {
                    throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
                }

                serviceList = serviceList.Where(s=>s.OrganizationId > 0).ToList();

                foreach(var service in serviceList)
                {
                    var s = _mygovReports.Find(r => r.OrganizationId == service.OrganizationId && r.ServiceId == service.ServiceId && r.Year == service.Year && r.Part == service.Part).FirstOrDefault();
                    if(s != null)
                    {
                        s.AllRequests = service.AllRequests;
                        s.LateRequests = service.LateRequests;

                        _mygovReports.Update(s);
                    }
                    else
                    {
                        _mygovReports.Add(service);
                    }
                }

            }
            return true;
        }

        public async Task<bool> UpdateMyGovReportDetails(int deadlineId)
        {
            List<MygovReportsDetail> addList = new List<MygovReportsDetail>();

            var deadline = _deadline.Find(o => o.Id == deadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
            var orgList = _organization.GetAll();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var byteArray = Encoding.ASCII.GetBytes("elektron.hukumat.loyihalarini.boshqarish.markazi.207322159:QaMZo\\*L.)}Yk&@bjH3Qm~7L3T~\"=:JJEm&hCiBQ");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            int part = 0;
            if (deadline.Quarter == Domain.Enums.Quarters.First || deadline.Quarter == Domain.Enums.Quarters.Second)
                part = 1;
            if (deadline.Quarter == Domain.Enums.Quarters.Third || deadline.Quarter == Domain.Enums.Quarters.Fourth)
                part = 2;

            var url = Links.MyGovServices + "year=" + deadline.Year + "&part=" + part+ "&type=detail";

            var response = await client.GetAsync(url).ConfigureAwait(false);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var responseResult = JsonConvert.DeserializeObject<List<OrgserviceDetail>>(jsonString).AsEnumerable();
                    
                    foreach(var item in responseResult)
                    {
                        addList.Add(new MygovReportsDetail
                        {
                            TaskId = item.DeadlineTask.Id,
                            ServiceId = item.MyGovService.Id,
                            ServiceName = item.MyGovService.Name,
                            DeadlineFrom = item.DeadlineTask.DeadlineFrom,
                            DeadlineTo = item.DeadlineTask.DeadlineTo,
                            Year = deadline.Year,
                            Part = part,
                        });
                    }

                    var allRecords = _mygovReportsDetail.GetAll();

                    _db.Context.Set<MygovReportsDetail>().RemoveRange(allRecords);

                    _db.Context.Set<MygovReportsDetail>().AddRange(addList);


                    _db.Context.SaveChanges();
                }
                catch
                {
                    throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
                }
            }

            return true;
        }
    }
}
