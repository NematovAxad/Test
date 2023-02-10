using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Requirement> Requirements { get; set; }
    }
    public class Requirement
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string SiteLink1 { get; set; }
        public string ScreenLink1 { get; set; }

        public string SiteLink2 { get; set; }
        public string ScreenLink2 { get; set; }

        public string SiteLink3 { get; set; }
        public string ScreenLink3 { get; set; }

        public string Comment { get; set; }
        public Steps RequirementStatus { get; set; }
    }
}
