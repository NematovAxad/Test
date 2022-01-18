using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.Ranking
{
    public class RankCommand:IRequest<RankCommandResult>
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
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public int FieldId { get; set; }
        public int SubFieldId { get; set; }
        public int ElementId { get; set; }
        public double Rank { get; set; }
        public bool IsException { get; set; }
        public string Comment { get; set; }
        public bool Switch { get; set; }
        public int SwitchValue { get; set; }
    }
}
