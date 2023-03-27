using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.Ranking
{
    public class DeadlineCommand:IRequest<DeadlineCommandResult>
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
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public DateTime SecondSectionDeadlineDate { get; set; }
        public DateTime ThirdSectionDeadlineDate { get; set; }
        public DateTime FifthSectionDeadlineDate { get; set; }
        public DateTime SixthSectionDeadlineDate { get; set; }
        public DateTime OperatorDeadlineDate { get; set; }
        public bool IsActive { get; set; }
    }
}
