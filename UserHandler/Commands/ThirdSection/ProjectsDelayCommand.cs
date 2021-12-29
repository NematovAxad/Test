using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class ProjectsDelayCommand:IRequest<ProjectsDelayCommandResult>
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
        public string ProjectName { get; set; }
        public string ProjectDocumentNumber { get; set; }
        public DateTime ProjectDocumentDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public string ProjectApplyingMechanism { get; set; }
        public DateTime ProjectApplyingDate { get; set; }
        public string ProjectFinancingSource { get; set; }
        public long ProjectPrice { get; set; }
        public long ProvidedFund { get; set; }
    }
}
