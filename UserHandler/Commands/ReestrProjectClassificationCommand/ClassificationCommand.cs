using Domain.Enums;
using Domain.Models.SecondSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrProjectClassificationCommandResult;

namespace UserHandler.Commands.ReestrProjectClassificationCommand
{
    public class ClassificationCommand:IRequest<ClassificationCommandResult>
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
        public int ParentId { get; set; }
        public ReestrProjectClassificationType ClassificationType { get; set; }
        public string ClassificationUri { get; set; }
        public string FilePath { get; set; }
    }
}
