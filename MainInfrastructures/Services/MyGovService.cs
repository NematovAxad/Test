using Domain;
using Domain.IntegrationLinks;
using Domain.Models;
using Domain.MyGovModels;
using Domain.States;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Services
{
    public class MyGovService : IMyGovService
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;

        public MyGovService(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline)
        {
            _organization = organization;
            _deadline = deadline;
        }

        public async Task<List<OrgServiceRecordsResult>> OrgServiceReport(int orgId, int deadlineId)
        {
            List<OrgServiceRecordsResult> serviceList = new List<OrgServiceRecordsResult>();

            var organization = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (organization == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(o => o.Id == deadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var byteArray = Encoding.ASCII.GetBytes("elektron.hukumat.loyihalarini.boshqarish.markazi.207322159:QaMZo\\*L.)}Yk&@bjH3Qm~7L3T~\"=:JJEm&hCiBQ");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            
            int part = 0;
            if (deadline.Quarter == Domain.Enums.Quarters.First || deadline.Quarter == Domain.Enums.Quarters.Second)
                part = 1;
            if(deadline.Quarter == Domain.Enums.Quarters.Third || deadline.Quarter == Domain.Enums.Quarters.Fourth)
                part = 2;

            var url = Links.MyGovServices + "year=" + deadline.Year + "&part=" + part;

            var response = await client.GetAsync(url).ConfigureAwait(false);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var responseResult = JsonConvert.DeserializeObject<List<OrgserviceRecord>>(jsonString).AsEnumerable();
                    responseResult = responseResult.Where(r => r.MainOrganization.Id == organization.MyGovId).AsEnumerable();

                    foreach (var i in responseResult)
                    {
                        if (serviceList.Any(r => r.ServiceId == i.MyGovService.Id))
                        {

                            var item = serviceList.Where(r => r.ServiceId == i.MyGovService.Id).FirstOrDefault();

                            item.AllRequests = item.AllRequests + i.Tasks.All;
                            item.LateRequests = item.LateRequests + i.Tasks.Deadline;
                        }
                        else
                        {
                            serviceList.Add(new OrgServiceRecordsResult
                            {
                                Id = i.MainOrganization.Id,
                                ServiceId = i.MyGovService.Id,
                                Name = i.MainOrganization.Name,
                                ServiceName = i.MyGovService.Name,
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
            }
            return serviceList;
        }
    }
}
