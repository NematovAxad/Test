using Domain.Enums;
using Domain.Models.SecondSection;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserHandler.Commands.ReestrProjectIdentityCommand
{
    public class ReestrProjectIdentityCommand:IRequest<ReestrProjectIdentityCommandResult>
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
        public int ReestrProjectId { get; set; }
        public bool Exist { get; set; }
        public string OrgComment { get; set; }
        public int AllItems { get; set; }
        public int ExceptedItems { get; set; }
        public string ExpertComment { get; set; }
    }
}
