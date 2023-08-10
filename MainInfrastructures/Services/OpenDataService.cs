using Domain.IntegrationLinks;
using Domain.OpenDataModels;
using Domain.States;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Models.FirstSection;
using Domain.Models.SecondSection;
using EntityRepository;
using JohaRepository;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using SB.Common.Extensions;

namespace MainInfrastructures.Services
{
    public class OpenDataService:IOpenDataService
    {
        private readonly IOrganizationService _organizationService;
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OpenDataTable, int> _openDataTable;
        private readonly IDataContext _db;

        public OpenDataService(IOrganizationService organizationService, IRepository<Organizations, int> organizations, IRepository<OpenDataTable, int> openDataTabl, IDataContext db)
        {
            _organizationService = organizationService;
            _organizations = organizations;
            _openDataTable = openDataTabl;
            _db = db;
        }
        
        public async Task<OpenDataQueryResult> OpenDataApi(OpenDataQuery model)
        {
            var result = new OpenDataQueryResult() {Data = new List<Data>() };

            var organization = _organizations.Find(o => o.UserServiceId == model.OrgId).FirstOrDefault();
            
            if (organization == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var tables = _openDataTable.Find(t => t.OrganizationId == organization.Id)
                .Include(mbox => mbox.Organizations).ToList();

            foreach (var table in tables)
            {
                result.Count++;
                result.Data.Add(new Data
                {
                    DataName = table.TableName,
                    OrgName = table.Organizations.ShortName,
                    Id = table.TableId,
                    UpdateDate = table.UpdateDate,
                    Status = table.Status,
                    Link = table.Link
                });
            }
            if(tables.Count > 0)
                result.LastUpdateTime = tables[0].TableLastUpdateDate;


            return await Task.FromResult(result);
        }
        
        public async Task<bool> UpdateOpenDataTable()
        {
            var all = _openDataTable.GetAll();
            _db.Context.Set<OpenDataTable>().RemoveRange(all);
            _db.Context.SaveChanges();

            List<OpenDataTable> addList = new List<OpenDataTable>();
            var organizations = _organizations.Find(o => o.IsActive == true && o.IsIct == true).ToList();

            foreach (Organizations o in organizations)
            {
                if(o.Id==131)
                {

                }
                var result = new OpenDataQueryResult();

                try
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);

                    var url = Links.OpenDataurl;
                    if (o.Id != 0)
                    {
                        url = url + "?orgId=" + o.UserServiceId.ToString();
                    }

                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var obj = JObject.Parse(jsonString);
                        var responseResult = JsonConvert.DeserializeObject<OpenDataResultModel>(jsonString);
                        result.Count = responseResult.Result.Count;
                        result.Data = responseResult.Result.Data;
                        foreach (Data data in result.Data)
                        {
                            OpenDataTable addModel = new OpenDataTable()
                            {
                            OrganizationId = o.Id,
                            TableId = data.Id,
                            TableName = data.DataName,
                            UpdateDate = data.UpdateDate,
                            Status = data.Status,
                            Link = data.Link,
                            TableLastUpdateDate = DateTime.Now
                            };
                            addList.Add(addModel);
                        }
                    }
                    else
                    {
                        throw ErrorStates.NotResponding();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            _db.Context.Set<OpenDataTable>().AddRange(addList);
            _db.Context.SaveChanges();
            
            return await Task.FromResult(true);
        }
    }
}
