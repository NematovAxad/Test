using Domain.IntegrationLinks;
using Domain.OpenDataModels;
using Domain.States;
using MainInfrastructures.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Services
{
    public class OpenDataService:IOpenDataService
    {
        public async Task<OpenDataQueryResult> OpenDataApi(OpenDataQuery model)
        {
            var result = new OpenDataQueryResult();

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);

                var url = Links.OpenDataurl;
                if (model.OrgId != 0)
                {
                    url = url + "?orgId=" + model.OrgId.ToString();
                }

                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (response != null || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(jsonString);
                    var responseResult = JsonConvert.DeserializeObject<OpenDataResultModel>(jsonString);
                    result.Count = responseResult.Result.Count;
                    result.Data = responseResult.Result.Data;
                    return result;
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
    }
}
