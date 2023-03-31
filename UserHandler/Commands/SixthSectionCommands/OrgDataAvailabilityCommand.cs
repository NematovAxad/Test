using Domain.Enums;
using Domain.Models.FirstSection;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MediatR;
using UserHandler.Results.SixthSectionResults;
using System.Text.Json.Serialization;

namespace UserHandler.Commands.SixthSectionCommands
{
    public class OrgDataAvailabilityCommand:IRequest<OrgDataAvailabilityCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserOrgId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string UserPinfl { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<string> UserPermissions { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public EventType EventType { get; set; }

        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public string Section { get; set; }

        public DataAvailability DataAvailability { get; set; }

        public bool DataRelevance { get; set; }

        public string ExpertComment { get; set; }
    }
}
