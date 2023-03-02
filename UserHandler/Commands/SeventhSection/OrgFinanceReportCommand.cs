using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Commands.SeventhSection
{
    public class OrgFinanceReportCommand:IRequest<OrgFinanceReportCommandResult>
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

        public double FullYearBudget { get; set; }

        public double FullYearSpentBudgetPercent { get; set; }

        public double FullYearDigitalizationBudgetPercent { get; set; }
    }
}
