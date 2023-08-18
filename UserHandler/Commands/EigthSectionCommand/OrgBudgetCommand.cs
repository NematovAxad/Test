using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Commands.EigthSectionCommand
{
    public class OrgBudgetCommand : IRequest<OrgBudgetCommandResult>
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


        public double PersonalBudgetPlan1 { get; set; }

        public double PersonalBudgetFact1 { get; set; }

        public double LocalInvestmentBudgetPlan1 { get; set; }

        public double LocalInvestmentBudgetFact1 { get; set; }

        public double ForeignBudgetPlan1 { get; set; }

        public double ForeignBudgetFact1 { get; set; }

        public double OtherBudgetPlan1 { get; set; }

        public double OtherBudgetFact1 { get; set; }



        public double PersonalBudgetPlan2 { get; set; }

        public double PersonalBudgetFact2 { get; set; }

        public double LocalInvestmentBudgetPlan2 { get; set; }

        public double LocalInvestmentBudgetFact2 { get; set; }

        public double ForeignBudgetPlan2 { get; set; }

        public double ForeignBudgetFact2 { get; set; }

        public double OtherBudgetPlan2 { get; set; }

        public double OtherBudgetFact2 { get; set; }



        public double PersonalBudgetPlan3 { get; set; }

        public double PersonalBudgetFact3 { get; set; }

        public double LocalInvestmentBudgetPlan3 { get; set; }

        public double LocalInvestmentBudgetFact3 { get; set; }

        public double ForeignBudgetPlan3 { get; set; }

        public double ForeignBudgetFact3 { get; set; }

        public double OtherBudgetPlan3 { get; set; }

        public double OtherBudgetFact3 { get; set; }



        public double PersonalBudgetPlan4 { get; set; }

        public double PersonalBudgetFact4 { get; set; }

        public double LocalInvestmentBudgetPlan4 { get; set; }

        public double LocalInvestmentBudgetFact4 { get; set; }

        public double ForeignBudgetPlan4 { get; set; }

        public double ForeignBudgetFact4 { get; set; }

        public double OtherBudgetPlan4 { get; set; }

        public double OtherBudgetFact4 { get; set; }

    }
}
