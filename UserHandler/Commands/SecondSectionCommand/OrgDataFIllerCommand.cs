using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SecondSectionCommandResult;

namespace UserHandler.Commands.SecondSectionCommand
{
    public class OrgDataFillerCommand:IRequest<OrgDataFillerCommandResult>
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
        public int FieldId { get; set; }
        public int DeadlineId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Contacts { get; set; }
    }
}
