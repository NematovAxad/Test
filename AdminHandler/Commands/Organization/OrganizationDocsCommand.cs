using AdminHandler.Results.Organization;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.Organization
{
    public class OrganizationDocsCommand:IRequest<OrganizationDocsCommandResult>
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
        public string DocumentNo { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string MainPurpose { get; set; }
        public string FilePath { get; set; }
    }
}
