using Domain;
using Domain.Enums;
using MediatR;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MonitoringHandler.Commands.StructureCommands
{
    public class StageCommand:IRequest<StageCommandResult>
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
        public string NameRu { get; set; }
        public string NameUz { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StageStatus StageStatus { get; set; }
        public int ProjectId { get; set; }
        public int CreationUserId { get; set; }
        public string CreationUserName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
