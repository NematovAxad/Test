﻿using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class OrgSocialParametersCommand:IRequest<OrgSocialParametersCommandResult>
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