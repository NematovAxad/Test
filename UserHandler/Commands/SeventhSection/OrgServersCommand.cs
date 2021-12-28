using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Commands.SeventhSection
{
    public class OrgServersCommand:IRequest<OrgServersCommandResult>
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
        public string ServerType { get; set; }
        public string ServerBrand { get; set; }
        public string ServerConfig { get; set; }
        public string ServerAutomaticTasks { get; set; }
        public string NumberOfServers { get; set; }
    }
}
