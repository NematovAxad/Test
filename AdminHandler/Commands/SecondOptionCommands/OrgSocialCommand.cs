using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class OrgSocialCommand:IRequest<OrgSocialCommandResult>
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
        public string MessengerLink { get; set; }
        public bool? OrgFullName { get; set; }
        public bool? OrgLegalSite { get; set; }
        public bool? OrgPhone { get; set; }
        public bool? OrgLegalAddress { get; set; }
        public bool? OrgEmail { get; set; }
        public bool? LinksToOtherSocials { get; set; }
        public string Link1 { get; set; }
        public bool Post1 { get; set; }
        public string Post1Link { get; set; }
        public string Post1Screenshot { get; set; }
        public string Link2 { get; set; }
        public bool Post2 { get; set; }
        public string Post2Link { get; set; }
        public string Post2Screenshot { get; set; }
        public string Link3 { get; set; }
        public bool Post3 { get; set; }
        public string Post3Link { get; set; }
        public string Post3Screenshot { get; set; }
        public string Link4 { get; set; }
        public bool Post4 { get; set; }
        public string Post4Link { get; set; }
        public string Post4Screenshot { get; set; }
        public string Link5 { get; set; }
        public bool Post5 { get; set; }
        public string Post5Link { get; set; }
        public string Post5Screenshot { get; set; }
        public bool? Pool { get; set; }
        public bool IsMain { get; set; }
        public bool? Verified { get; set; }
        public bool? SyncronizedPosts { get; set; }
    }
}
