using Domain.Enums;
using Domain.Models.SecondSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrProjectClassificationCommandResult;
using UserHandler.Results.ReestrProjectEfficiencyResult;

namespace UserHandler.Commands.ReestrProjectEfficiencyCommand
{
    public class ProjectEfficiencyCommand : IRequest<ProjectEfficiencyCommandResult>
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
        public int ParentId { get; set; }
        public ProjectEfficiencyType EfficiencyType { get; set; }
        public string FilePath { get; set; }
        public string OrgComment { get; set; }
    }
}
