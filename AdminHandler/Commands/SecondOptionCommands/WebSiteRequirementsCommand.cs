using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class WebSiteRequirementsCommand:IRequest<WebSiteRequirementsCommandResult>
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
        public Steps Requirement1 { get; set; }
        public Steps Requirement2 { get; set; }
        public Steps Requirement3 { get; set; }
        public Steps Requirement4 { get; set; }
        public Steps Requirement5 { get; set; }
        public Steps Requirement6 { get; set; }
        public Steps Requirement7 { get; set; }
        public Steps Requirement8 { get; set; }
        public Steps Requirement9 { get; set; }
        public Steps Requirement10 { get; set; }
        public Steps Requirement11 { get; set; }
        public Steps Requirement12 { get; set; }
        public Steps Requirement13 { get; set; }
        public Steps Requirement14 { get; set; }
        public Steps Requirement15 { get; set; }
        public Steps Requirement16 { get; set; }
        public Steps Requirement17 { get; set; }
        public Steps Requirement18 { get; set; }
        public Steps Requirement19 { get; set; }
        public Steps Requirement20 { get; set; }
        public Steps Requirement21 { get; set; }
        public Steps Requirement22 { get; set; }
        public Steps Requirement23 { get; set; }
        public Steps Requirement24 { get; set; }
        public Steps Requirement25 { get; set; }
        public Steps Requirement26 { get; set; }
        public Steps Requirement27 { get; set; }
        public Steps Requirement28 { get; set; }
        public Steps Requirement29 { get; set; }
        public Steps Requirement30 { get; set; }
        public Steps Requirement31 { get; set; }
        public Steps Requirement32 { get; set; }
        public Steps Requirement33 { get; set; }
        public Steps Requirement34 { get; set; }
        public Steps Requirement35 { get; set; }
        public Steps Requirement36 { get; set; }
        public Steps Requirement37 { get; set; }
        public Steps Requirement38 { get; set; }
        public Steps Requirement39 { get; set; }
        public Steps Requirement40 { get; set; }
        public Steps Requirement41 { get; set; }
        public Steps Requirement42 { get; set; }
        public Steps Requirement43 { get; set; }
        public Steps Requirement44 { get; set; }
        public Steps Requirement45 { get; set; }
    }
}
