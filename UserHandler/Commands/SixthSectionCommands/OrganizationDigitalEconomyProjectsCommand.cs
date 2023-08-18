using Domain.Enums;
using Domain.Models.FirstSection;
using Domain.Models.SixthSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Commands.SixthSectionCommands
{
    public class OrganizationDigitalEconomyProjectsCommand:IRequest<OrganizationDigitalEconomyProjectsCommandResult>
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

        public string ProjectName { get; set; }

        public string BasisFilePath { get; set; }

        public string Comment { get; set; }

    }
}
