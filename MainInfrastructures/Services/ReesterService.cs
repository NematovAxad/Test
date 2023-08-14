using Domain.IntegrationLinks;
using Domain.Models.FirstSection;
using Domain.ReesterModels;
using Domain.States;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Models.FifthSection.ReestrModels;
using Domain.MonitoringModels.Models;
using EntityRepository;

namespace MainInfrastructures.Services
{
    public class ReesterService : IReesterService
    {
        private readonly IRepository<Organizations, int> _org;
        private readonly IRepository<ReestrProjectPassport, int> _reestrPassport;
        private readonly IRepository<ReestrProjectPassportDetails, int> _reestrPassportDetails;
        private readonly IDataContext _db;

        public ReesterService(IRepository<Organizations, int> org, IDataContext db, IRepository<ReestrProjectPassport, int> reestrPassport, IRepository<ReestrProjectPassportDetails, int> reestrPassportDetails)
        {
            _org = org;
            _reestrPassport = reestrPassport;
            _reestrPassportDetails = reestrPassportDetails;
            _db = db;
        }

        public async Task<FirstRequestQueryResult> FirstRequest(FirstRequestQuery model)
        {
            var organization = _org.Find(d => d.Id == model.OrgId).FirstOrDefault();
            if (organization == null)
                throw ErrorStates.NotFound("organization");

            var result = new FirstRequestQueryResult();
            var cts = new CancellationTokenSource();
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);

                var byteArray = Encoding.ASCII.GetBytes("reestr:Tf0TJ&3C78B1&rj3WTOD8j3cvhk5*1Vs$x#kF#GB");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var url = Links.ReesterFirstLink;
                
                url = url + "?OrgId=" + organization.UserServiceId.ToString()+"&Page="+model.Page.ToString()+"&Limit="+model.Limit.ToString();

                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(jsonString);
                    result = JsonConvert.DeserializeObject<FirstRequestQueryResult>(jsonString);
                    
                    return result;
                }
                else
                {
                    throw ErrorStates.NotResponding();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex);
                throw ex;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task<SecondRequestQueryResult> SecondRequest(SecondRequestQuery model)
        {
            var result = new SecondRequestQueryResult();
            var cts = new CancellationTokenSource();
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);

                var byteArray = Encoding.ASCII.GetBytes("reestr:Tf0TJ&3C78B1&rj3WTOD8j3cvhk5*1Vs$x#kF#GB");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var url = Links.ReesterSecondLink;

                url = url + "?id=" + model.Id.ToString();

                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(jsonString);
                    result = JsonConvert.DeserializeObject<SecondRequestQueryResult>(jsonString);

                    return result;
                }
                else
                {
                    throw ErrorStates.NotResponding();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex);
                throw ex;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task<bool> UpdateReestrTable()
        {
            var allPassports = _reestrPassport.GetAll();
            var allPassportsDetails = _reestrPassportDetails.GetAll();
            _db.Context.Set<ReestrProjectPassport>().RemoveRange(allPassports);
            _db.Context.Set<ReestrProjectPassportDetails>().RemoveRange(allPassportsDetails);
            _db.Context.SaveChanges();
            
            List<ReestrProjectPassport> addPassportsList = new List<ReestrProjectPassport>();
            List<ReestrProjectPassportDetails> addPassportDetailsList = new List<ReestrProjectPassportDetails>();
            var organizations = _org.Find(o => o.IsActive == true && o.IsIct == true).ToList();

            foreach (Organizations o in organizations)
            {
                int page = 1;
                int limit = 10000;
                try
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);

                    var byteArray = Encoding.ASCII.GetBytes("reestr:Tf0TJ&3C78B1&rj3WTOD8j3cvhk5*1Vs$x#kF#GB");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var url = Links.ReesterFirstLink;
                
                    url = url + "?OrgId=" + o.UserServiceId.ToString()+"&Page="+page.ToString()+"&Limit="+limit.ToString();

                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var obj = JObject.Parse(jsonString);
                        var result = JsonConvert.DeserializeObject<FirstRequestQueryResult>(jsonString);

                        foreach (var project in result.Items)
                        {
                            addPassportsList.Add(new ReestrProjectPassport()
                            {
                                OrganizationId = o.Id,
                                ReestrProjectId = project.Id,
                                FullName = project.FullName,
                                ShortName = project.ShortName,
                                PassportStatus = project.PassportStatus,
                                HasTerms = project.HasTerms,
                                HasExpertise = project.HasExpertise,
                                LinkForSystem = project.LinkForSystem,
                                UpdateTime = DateTime.Now,
                            });
                            try
                            {
                                HttpClientHandler clientHandlerSecon = new HttpClientHandler();
                                clientHandlerSecon.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                                HttpClient clientSecond = new HttpClient(clientHandlerSecon);

                                var byteArraySecond = Encoding.ASCII.GetBytes("reestr:Tf0TJ&3C78B1&rj3WTOD8j3cvhk5*1Vs$x#kF#GB");
                                clientSecond.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArraySecond));
                                var urlSecond = Links.ReesterSecondLink;

                                urlSecond = urlSecond + "?id=" + project.Id.ToString();

                                var responseSecond = await clientSecond.GetAsync(urlSecond).ConfigureAwait(false);
                                if (responseSecond != null && responseSecond.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var jsonStringSecond = await responseSecond.Content.ReadAsStringAsync();
                                    var objSecond = JObject.Parse(jsonStringSecond);
                                    var resultSecond = JsonConvert.DeserializeObject<SecondRequestQueryResult>(jsonStringSecond);

                                    addPassportDetailsList.Add(new ReestrProjectPassportDetails()
                                    {
                                        OrganizationId = o.Id,
                                        ReestrProjectId = project.Id,
                                        FullName = resultSecond.FullName,
                                        ShortName = resultSecond.ShortName,
                                        PassportStatus = resultSecond.PassportStatus,
                                        BasisName = resultSecond.BasisName,
                                        Tasks = resultSecond.Tasks,
                                        IsInterdepartmentalInformationSystem = resultSecond.IsInterdepartmentalInformationSystem,
                                        CybersecurityExpertise = resultSecond.CybersecurityExpertise,
                                        UpdateTime = DateTime.Now,
                                    });
                                }
                                else
                                {
                                    throw ErrorStates.NotResponding();
                                }
                            }
                            catch (HttpRequestException ex)
                            {
                                Console.WriteLine("Error: " + ex);
                                throw;
                            }
                            
                        }
                        
                    }
                    else
                    {
                        throw ErrorStates.Error(UIErrors.ReestrApierrorCase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error: " + ex);
                    throw;
                }
            }
            
            _db.Context.Set<ReestrProjectPassport>().AddRange(addPassportsList);
            _db.Context.Set<ReestrProjectPassportDetails>().AddRange(addPassportDetailsList);
            await _db.Context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<FirstRequestQueryResult> FirstRequestNew(FirstRequestQuery model)
        {
            var organization = _org.Find(d => d.Id == model.OrgId).FirstOrDefault();
            if (organization == null)
                throw ErrorStates.NotFound("organization");

            var result = new FirstRequestQueryResult(){Items = new List<FirstRequestResultModel>()};

            var orgProjects = _reestrPassport.Find(p => p.OrganizationId == model.OrgId).OrderByDescending(p => p.ReestrProjectId)
                .ToList();
            var returnPart = orgProjects.Skip((int)(model.Page - 1) * model.Limit).Take(model.Limit)
                .OrderByDescending(p => p.ReestrProjectId).ToList();

            result.Count = orgProjects.Count;
            result.TotalPages = (int)orgProjects.Count / model.Limit;
            
            if (returnPart.Count > 0)
                result.UpdateTime = returnPart[0].UpdateTime;

            if (orgProjects.Count > 0 && orgProjects.Count % model.Limit != 0)
                result.TotalPages += 1;
                
            if(result.TotalPages == 0)
                if (orgProjects.Count > 0)
                    result.TotalPages = 1;
            
            foreach (var project in returnPart)
            {
                result.Items.Add(new FirstRequestResultModel()
                {
                    Id = project.ReestrProjectId,
                    FullName = project.FullName,
                    ShortName = project.ShortName,
                    PassportStatus = project.PassportStatus,
                    HasTerms = project.HasTerms,
                    LinkForSystem = project.LinkForSystem
                });
            }

            return await Task.FromResult(result);
        }

        public async Task<SecondRequestQueryResult> SecondRequestNew(SecondRequestQuery model)
        {
            var result = new SecondRequestQueryResult(){RepresentingGovernmentAgencyList = new List<string>(), IdentityTypes = new List<IdentityGetModel>()};
            var reestrProjectDetails = _reestrPassportDetails.Find(p => p.ReestrProjectId == model.Id).FirstOrDefault();
            if (reestrProjectDetails != null)
            {
                result.Id = reestrProjectDetails.ReestrProjectId;
                result.FullName = reestrProjectDetails.FullName;
                result.ShortName = reestrProjectDetails.ShortName;
                result.PassportStatus = reestrProjectDetails.PassportStatus;
                result.BasisName = reestrProjectDetails.BasisName;
                result.Tasks = reestrProjectDetails.Tasks;
                result.IsInterdepartmentalInformationSystem = reestrProjectDetails.IsInterdepartmentalInformationSystem;
                result.CybersecurityExpertise = reestrProjectDetails.CybersecurityExpertise;
                result.UpdateTime = reestrProjectDetails.UpdateTime;
            }

            return await Task.FromResult(result);
        }
    }
}
