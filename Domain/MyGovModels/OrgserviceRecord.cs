using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.MyGovModels
{
    public class OrgserviceRecord
    {
        [JsonProperty(PropertyName = "main_organisation")]
        public MyGovMainOrganization MainOrganization { get; set; }

        [JsonProperty(PropertyName = "organisation")]
        public MyGovOrganizationRecord OrganizationRecord { get; set; }

        [JsonProperty(PropertyName = "service")]
        public MyGovService MyGovService { get; set; }

        [JsonProperty(PropertyName = "tasks")]
        public Tasks Tasks { get; set; }
    }

    public class OrgserviceDetail
    {

        [JsonProperty(PropertyName = "organisation")]
        public MyGovOrganizationRecord OrganizationRecord { get; set; }

        [JsonProperty(PropertyName = "service")]
        public MyGovService MyGovService { get; set; }

        [JsonProperty(PropertyName = "deadline_task")]
        public DeadlineTask DeadlineTask { get; set; }
    }

    public class MyGovMainOrganization
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class MyGovOrganizationRecord
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class MyGovService
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Tasks
    {
        [JsonProperty(PropertyName = "all")]
        public int All { get; set; }

        [JsonProperty(PropertyName = "deadline")]
        public int Deadline { get; set; }
    }

    public class DeadlineTask
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "deadline_from")]
        public string DeadlineFrom { get; set; }

        [JsonProperty(PropertyName = "deadline_to")]
        public string DeadlineTo { get; set; }
    }
}
