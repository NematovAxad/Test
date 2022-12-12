using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CyberSecurityModels
{
    public class GetOrgRanksResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("norm")]
        public double Norm { get; set; }

        [JsonProperty("org")]
        public double Org { get; set; }

        [JsonProperty("tex")]
        public double Tex { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
