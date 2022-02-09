using Domain.Enums;
using MediatR;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MonitoringHandler.Commands.StructureCommands
{
    public class CommentCommand:IRequest<CommentCommandResult>
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
        public string Text { get; set; }
        public DateTime DateComment { get; set; }
        public int StageId { get; set; }
    }
}
