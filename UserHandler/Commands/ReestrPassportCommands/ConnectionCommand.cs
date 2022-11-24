using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Commands.ReestrPassportCommands
{
    public class ConnectionCommand:IRequest<ConnectionCommandResult>
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

        public int ReestrProjectConnectionId { get; set; }

        public ReestrProjectConnectionType ReestrProjectConnectionType { get; set; }

        public string PlatformReestrId { get; set; }

        public string FilePath { get; set; }
    }
}
