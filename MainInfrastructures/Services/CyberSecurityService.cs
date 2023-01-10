using Domain.CyberSecurityModels;
using Domain.Models;
using Domain.Models.Ranking.Administrations;
using Domain.MonitoringModels.Models;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using MainInfrastructures.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Services
{
    public class CyberSecurityService : ICyberSecurityService
    {
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Organizations, int> _org;
        private readonly IRepository<GRankTable, int> _gRank;
        private readonly IRepository<XRankTable, int> _xRank;
        private readonly IRepository<ARankTable, int> _aRank;

        private IDataContext _db;

        public CyberSecurityService(IRepository<Deadline, int> deadline, IRepository<Organizations, int> org, IRepository<GRankTable, int> gRank, IRepository<XRankTable, int> xRank, IRepository<ARankTable, int> aRank, IDataContext db)
        {
            _deadline = deadline;
            _org = org;
            _gRank = gRank;
            _xRank = xRank;
            _aRank = aRank;
            _db = db;
        }

        public async Task<bool> GetOrgRank(GetOrgRanksQuery model)
        {
            string token = await GetToken();

            var deadline = _deadline.Find(d => d.Id == model.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            
            //var organization = _org.Find(o => o.Id == model.OrgId).FirstOrDefault();
            //if(organization == null)
            //    throw ErrorStates.NotFound("organization");

            List<GetOrgRanksResult> fullList = new List<GetOrgRanksResult>();

            var dataParameters = await GetRanks(deadline, token, 1);
            for(int i =0; i < dataParameters.Meta.Pagination.TotalPages; i++)
            {
                OrgRanks orgGroups = await GetRanks(deadline, token, i+1);
                fullList.AddRange(orgGroups.Data);
            }


            await SetRanks(fullList, deadline);

            //GetOrgRanksResult result = new GetOrgRanksResult();

            //var orgRank = fullList.Where(l => l.Id == organization.CyberSecurityId).FirstOrDefault();
            //if(orgRank != null)
            //{
            //    result = orgRank;
            //}
            
    
            //return result;

            return true;
        }

        public async Task<string> GetToken()
        {
            var model = new
            {
                userName = "api.client@uzinfocom.uz",
                password = "A!Za489Tt8wu"
            };
            try
            {
                HttpClient client = new HttpClient();


                var url = "https://sm.csec.uz/api/v1/login";
                var parameters = new Dictionary<string, string> { { "username", model.userName }, { "password", model.password} };
                var encodedContent = new FormUrlEncodedContent(parameters);
                //var jsonObject = JsonConvert.SerializeObject(model);
                var response = await client.PostAsync(url, encodedContent).ConfigureAwait(true);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var resultFormOneId = JsonConvert.DeserializeObject<TokenResult>(jsonString);
                    return resultFormOneId.AccessToken;
                }
                else
                {
                    throw ErrorStates.CyberSecurityServiceNotWorking();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public async Task<OrgRanks> GetRanks(Deadline deadline, string token, int page)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = "https://sm.csec.uz/api/v1/nis/rating";
                var parameters = new Dictionary<string, string> { { "quarter ", ((int)deadline.Quarter).ToString() }, { "year ", deadline.Year.ToString() }, {"page ", page.ToString()}, {"per_page ", "50" } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                var response = await client.GetAsync(url+"?"+parameters).ConfigureAwait(true);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var orgRanks = JsonConvert.DeserializeObject<OrgRanks>(jsonString);
                    return orgRanks;
                }
                else
                {
                    throw ErrorStates.CyberSecurityServiceNotWorking();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }


        public async Task<bool> SetRanks(List<GetOrgRanksResult> fullList, Deadline deadline)
        {

            foreach(var item in fullList)
            {
                var organization = _org.Find(o => o.CyberSecurityId == item.Id).FirstOrDefault();
                if(organization != null)
                {
                    if(organization.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                    {
                        var firstRank = _aRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 2 && r.FieldId == 8).FirstOrDefault();
                        var secondRank = _aRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 2 && r.FieldId == 9).FirstOrDefault();
                        var thirdRank = _aRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 2 && r.FieldId == 10).FirstOrDefault();


                        if (firstRank != null)
                        {
                            firstRank.Rank = item.Norm;

                            _aRank.Update(firstRank);
                        }
                        else
                        {
                            ARankTable addModelFirst = new ARankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Norm,
                                IsException = false,
                                SphereId = 2,
                                FieldId = 8,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _aRank.Add(addModelFirst);
                        }

                        
                        if (secondRank != null)
                        {
                            secondRank.Rank = item.Org;

                            _aRank.Update(secondRank);
                        }
                        else
                        {
                            ARankTable addModelSecond = new ARankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Org,
                                IsException = false,
                                SphereId = 2,
                                FieldId = 9,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _aRank.Add(addModelSecond);
                        }

                        
                        if (thirdRank != null)
                        {
                            thirdRank.Rank = item.Tex;

                            _aRank.Update(thirdRank);
                        }
                        else
                        {
                            ARankTable addModelThird = new ARankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Tex,
                                IsException = false,
                                SphereId = 2,
                                FieldId = 10,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _aRank.Add(addModelThird);
                        }
                    }
                    if (organization.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                    {
                        var firstRank = _gRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 24).FirstOrDefault();
                        var secondRank = _gRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 25).FirstOrDefault();
                        var thirdRank = _gRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 26).FirstOrDefault();


                        if (firstRank != null)
                        {
                            firstRank.Rank = item.Norm;

                            _gRank.Update(firstRank);
                        }
                        else
                        {
                            GRankTable addModelFirst = new GRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Norm,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 24,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _gRank.Add(addModelFirst);
                        }

                        
                        if (secondRank != null)
                        {
                            secondRank.Rank = item.Org;

                            _gRank.Update(secondRank);
                        }
                        else
                        {
                            GRankTable addModelSecond = new GRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Org,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 25,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _gRank.Add(addModelSecond);
                        }

                        
                        if (thirdRank != null)
                        {
                            thirdRank.Rank = item.Tex;

                            _gRank.Update(thirdRank);
                        }
                        else
                        {
                            GRankTable addModelThird = new GRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Tex,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 26,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _gRank.Add(addModelThird);
                        }
                        
                    }
                    if (organization.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                    {
                        
                        var firstRank = _xRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 24).FirstOrDefault();
                        var secondRank = _xRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 25).FirstOrDefault();
                        var thirdRank = _xRank.Find(r => r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == 3 && r.FieldId == 26).FirstOrDefault();



                        if (firstRank != null)
                        {
                            firstRank.Rank = item.Norm;

                            _xRank.Update(firstRank);
                        }
                        else
                        {
                            XRankTable addModelFirst = new XRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Norm,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 24,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _xRank.Add(addModelFirst);
                        }

                       
                        if (secondRank != null)
                        {
                            secondRank.Rank = item.Org;

                            _xRank.Update(secondRank);
                        }
                        else
                        {
                            XRankTable addModelSecond = new XRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Org,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 25,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _xRank.Add(addModelSecond);
                        }

                        
                        if (thirdRank != null)
                        {
                            thirdRank.Rank = item.Tex;

                            _xRank.Update(thirdRank);
                        }
                        else
                        {
                            XRankTable addModelThird = new XRankTable()
                            {
                                OrganizationId = organization.Id,
                                Year = deadline.Year,
                                Quarter = deadline.Quarter,
                                Rank = item.Tex,
                                IsException = false,
                                SphereId = 3,
                                FieldId = 26,
                                Comment = String.Empty,
                                SubFieldId = 0,
                                ElementId = 0
                            };

                            _xRank.Add(addModelThird);
                        }
                    }
                }
            }
            return true;
        }
    }

    public class TokenResult
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class OrgRanks
    {
        [JsonProperty("data")]
        public List<GetOrgRanksResult> Data { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        [JsonProperty("pagination")]
        public Pegination Pagination { get; set; }
    }
    public class Pegination
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
