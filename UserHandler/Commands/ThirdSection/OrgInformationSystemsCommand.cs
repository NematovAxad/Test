using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class OrgInformationSystemsCommand:IRequest<OrgInformationSystemsCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserOrgId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<string> UserPermissions { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string SystemFullName { get; set; }
        public string SystemShortName { get; set; }
        public ReestrStatus SystemStatus { get; set; }
        public string SystemId { get; set; }
        public string SystemLink { get; set; }
        public string SystemBasis { get; set; }
        public string SystemPurpose { get; set; }
        public bool SystemConnections { get; set; }
        public bool ClassifiersUsed { get; set; }
        public bool SystemUniqueIds { get; set; }
        public bool ExpertDecision { get; set; }
        public bool SybersecurityDecision { get; set; }
    }
}
