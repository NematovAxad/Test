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
    public class NormativeCommand:IRequest<NormativeCommandResult>
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
        public int Number { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public DateTime ApprovedDate { get; set; }
        public NormativeType NormativType { get; set; }
    }
}
