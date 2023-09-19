using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.OpenDataModels
{
    public class OpenDataResultModel
    {
        [JsonProperty(PropertyName = "result")]
        public ResultClass Result { get; set; }
    }
    public class ResultClass
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        [JsonProperty(PropertyName = "dataName")]
        public string DataName { get; set; }

        [JsonProperty(PropertyName = "orgName")]
        public string OrgName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "updateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty(PropertyName = "status")]
        public OpenDataTableStatus Status { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
    }
}
