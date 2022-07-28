using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class OrgSocialSitesCommand:IRequest<OrgSocialSitesCommandResult>
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
        public Social Social { get; set; }
        public SocialParameter SocialParameter { get; set; }
    }
    public class Social
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string MessengerLink { get; set; }
        public bool? Verified { get; set; }
    }
    public class SocialParameter
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public bool? OrgFullName { get; set; }
        public bool? OrgLegalSite { get; set; }
        public bool? OrgPhone { get; set; }
        public bool? OrgLegalAddress { get; set; }
        public bool? OrgEmail { get; set; }
        public bool? LinksToOtherSocials { get; set; }
        public bool? SyncronizedPosts { get; set; }
        public bool? Pool { get; set; }
    }
}
