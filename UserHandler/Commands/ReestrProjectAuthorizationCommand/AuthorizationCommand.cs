using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using UserHandler.Results.ReestrProjectClassificationCommandResult;

namespace UserHandler.Commands.ReestrProjectAuthorizationCommand
{
    public class AuthorizationCommand : IRequest<AuthorizationCommandResult>
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
        public ReestrProjectAuthorizationType AuthorizationType { get; set; }
        public string AuthorizationUri { get; set; }
        public string FilePath { get; set; }
    }
}
