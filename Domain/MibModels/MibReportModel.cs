using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MibModels
{
    public class MibReportModel
    {
        [JsonProperty(PropertyName = "api_name")]
        public string ApiName { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public string OwnerInn { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string ApiDescription { get; set; }

        [JsonProperty(PropertyName = "api_version")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "success_count")]
        public int SuccessCount { get; set; }

        [JsonProperty(PropertyName = "fail_count")]
        public int FailCount { get; set; }

        [JsonProperty(PropertyName = "overall")]
        public int Overall { get; set; }

        [JsonProperty(PropertyName = "success_share")]
        public double SuccessShare { get; set; }
    }
}
