using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;

namespace UserHandler.Commands.ReestrProjectAutomatedServicesCommand
{
    public class ReestrProjectServicesCommand:IRequest<ReestrProjectServicesCommandResult>
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
        public bool ProjectServiceExist { get; set; }
        public bool ProjectFunctionsExist { get; set; }
        public int AllItems { get; set; }
        public int ExceptedItems { get; set; }
        public string ExpertComment { get; set; }
    }
}
