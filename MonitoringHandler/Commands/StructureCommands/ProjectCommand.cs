using Domain;
using Domain.Enums;
using MediatR;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MonitoringHandler.Commands.StructureCommands
{
    public class ProjectCommand:IRequest<ProjectCommandResult>
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
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public MonitoringProjectStatus Status { get; set; }
        public string ProjectPurpose { get; set; }
        public string CostEffective { get; set; }
        public string Problem { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double VolumeForecastFunds { get; set; }
        public double RaisedFunds { get; set; }
        public double Payouts { get; set; }
        public int PerformencerId { get; set; }
        public int ApplicationId { get; set; }
    }
}
