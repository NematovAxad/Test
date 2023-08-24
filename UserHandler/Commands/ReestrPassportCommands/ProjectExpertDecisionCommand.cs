using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Commands.ReestrPassportCommands
{
    public class ProjectExpertDecisionCommand:IRequest<ProjectExpertDecisionCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string UserPinfl { get; set; }
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

        public int ReestrProjectId { get; set; }

        public bool Exist { get; set; }

        public string FilePath { get; set; }

        public string OrgComment { get; set; }

        public bool ExpertExcept { get; set; }

        public string ExpertComment { get; set; }
    }
}
