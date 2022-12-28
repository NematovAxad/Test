using Domain.CyberSecurityModels;
using Domain.Models;
using Domain.States;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public CyberSecurityService(IRepository<Deadline, int> deadline, IRepository<Organizations, int> org)
        {
            _deadline = deadline;
            _org = org;
        }

        public async Task<GetOrgRanksResult> GetOrgRank(GetOrgRanksQuery model)
        {
            string token = await GetToken();

            var deadline = _deadline.Find(d => d.Id == model.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            
            var organization = _org.Find(o => o.Id == model.OrgId).FirstOrDefault();
            if(organization == null)
                throw ErrorStates.NotFound("organization");

            List<GetOrgRanksResult> fullList = new List<GetOrgRanksResult>();

            var dataParameters = await GetRanks(deadline, token, 1);
            for(int i =0; i < dataParameters.Meta.Pagination.TotalPages; i++)
            {
                OrgRanks orgGroups = await GetRanks(deadline, token, i+1);
                fullList.AddRange(orgGroups.Data);
            }


            GetOrgRanksResult result = new GetOrgRanksResult();

            var orgRank = fullList.Where(l => l.Id == organization.CyberSecurityId).FirstOrDefault();
            if(orgRank != null)
            {
                result = orgRank;
            }
            
    
            return result;
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
