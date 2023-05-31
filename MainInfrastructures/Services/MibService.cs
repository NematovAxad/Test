using Domain.IntegrationLinks;
using Domain.Models.MibModels;
using Domain.Models.Organization;
using Domain.Models;
using Domain.States;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MainInfrastructures.Interfaces;
using Domain.Models.FirstSection;
using EntityRepository;
using JohaRepository;
using System.Globalization;
using Domain.MibModels;

namespace MainInfrastructures.Services
{
    public class MibService:IMibService
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<MibReport, int> _mibReport;
        private readonly IDataContext _db;

        public MibService(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<MibReport, int> mibReport, IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _mibReport = mibReport;
            _db = db;
        }

        public async Task<bool> MibReport(DateTime startTime, DateTime endTime)
        {
            List<MibReport> serviceList = new List<MibReport>();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var byteArray = Encoding.ASCII.GetBytes("nisreport:n1$R3P0r+");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));



            var url = Links.MibService + "date1=" + startTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + "&date2=" + endTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

            var response = await client.GetAsync(url).ConfigureAwait(false);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var responseResult = JsonConvert.DeserializeObject<List<MibReportModel>>(jsonString).AsEnumerable();

                    foreach (var i in responseResult)
                    {
                        serviceList.Add(new MibReport
                        {
                            ApiName = i.ApiName,
                            OwnerInn = i.OwnerInn,
                            ApiDescription = i.ApiDescription,
                            ApiVersion = i.ApiVersion,
                            SuccessCount = i.SuccessCount,
                            FailCount = i.FailCount,
                            Overall = i.Overall,
                            SuccessShare = i.SuccessShare
                        });

                    }
                }
                catch
                {
                    throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
                }

                var allRecords = _mibReport.GetAll();

                _db.Context.Set<MibReport>().RemoveRange(allRecords);

                _db.Context.Set<MibReport>().AddRange(serviceList);

                _db.Context.SaveChanges();

            }
            else 
            {
                return false;
            }

            return true;
        }
    }
}
