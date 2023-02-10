using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class ContentManagerCommand:IRequest<ContentManagerCommandResult>
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
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string FilePath { get; set; }
    }
}
