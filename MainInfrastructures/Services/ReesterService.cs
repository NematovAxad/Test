using Domain.IntegrationLinks;
using Domain.Models;
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

namespace MainInfrastructures.Services
{
    public class ReesterService : IReesterService
    {
        private readonly IRepository<Organizations, int> _org;

        public ReesterService(IRepository<Organizations, int> org)
        {
            _org = org;
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
                if (response != null || response.StatusCode == System.Net.HttpStatusCode.OK)
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
                if (response != null || response.StatusCode == System.Net.HttpStatusCode.OK)
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
    }
}
