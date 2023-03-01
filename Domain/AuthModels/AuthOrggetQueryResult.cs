using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AuthModels
{
    public class AuthOrggetQueryResult
    {
        [JsonProperty(PropertyName = "result")]
        public AuthOrgResult Result { get; set; }
    }

    public class AuthOrgResult
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<AuthOrgData> Data { get; set; }
    }

    public class AuthOrgData
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "nameRu")]
        public string NameRu { get; set; }
    }
}
