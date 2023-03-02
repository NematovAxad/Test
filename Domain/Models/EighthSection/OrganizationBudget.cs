using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.EighthSection
{
    [Table("organization_budget", Schema = "organizations")]
    public class OrganizationBudget : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("year")]
        public int Year { get; set; }

        #region 1 chorak

        [Column("personal_budget_plan1")]
        public double PersonalBudgetPlan1 { get; set; }

        [Column("personal_budget_fact1")]
        public double PersonalBudgetFact1 { get; set; }

        [Column("local_investment_budget_plan1")]
        public double LocalInvestmentBudgetPlan1 { get; set; }

        [Column("local_investment_budget_fact1")]
        public double LocalInvestmentBudgetFact1 { get; set; }

        [Column("foreign_budget_plan1")]
        public double ForeignBudgetPlan1 { get; set; }

        [Column("foreign_budget_fact1")]
        public double ForeignBudgetFact1 { get; set; }

        [Column("other_budget_plan1")]
        public double OtherBudgetPlan1 { get; set; }

        [Column("other_budget_fact1")]
        public double OtherBudgetFact1 { get; set; }

        [Column("all_plan1")]
        public double AllPlan1 { get; set; }

        [Column("all_fact1")]
        public double AllFact1 { get; set; }
        #endregion

        #region 2 chorak

        [Column("personal_budget_plan2")]
        public double PersonalBudgetPlan2 { get; set; }

        [Column("personal_budget_fact2")]
        public double PersonalBudgetFact2 { get; set; }

        [Column("local_investment_budget_plan2")]
        public double LocalInvestmentBudgetPlan2 { get; set; }

        [Column("local_investment_budget_fact2")]
        public double LocalInvestmentBudgetFact2 { get; set; }

        [Column("foreign_budget_plan2")]
        public double ForeignBudgetPlan2 { get; set; }

        [Column("foreign_budget_fact2")]
        public double ForeignBudgetFact2 { get; set; }

        [Column("other_budget_plan2")]
        public double OtherBudgetPlan2 { get; set; }

        [Column("other_budget_fact2")]
        public double OtherBudgetFact2 { get; set; }

        [Column("all_plan2")]
        public double AllPlan2 { get; set; }

        [Column("all_fact2")]
        public double AllFact2 { get; set; }
        #endregion

        #region 3 chorak

        [Column("personal_budget_plan3")]
        public double PersonalBudgetPlan3 { get; set; }

        [Column("personal_budget_fact3")]
        public double PersonalBudgetFact3 { get; set; }

        [Column("local_investment_budget_plan3")]
        public double LocalInvestmentBudgetPlan3 { get; set; }

        [Column("local_investment_budget_fact3")]
        public double LocalInvestmentBudgetFact3 { get; set; }

        [Column("foreign_budget_plan3")]
        public double ForeignBudgetPlan3 { get; set; }

        [Column("foreign_budget_fact3")]
        public double ForeignBudgetFact3 { get; set; }

        [Column("other_budget_plan3")]
        public double OtherBudgetPlan3 { get; set; }

        [Column("other_budget_fact3")]
        public double OtherBudgetFact3 { get; set; }

        [Column("all_plan3")]
        public double AllPlan3 { get; set; }

        [Column("all_fact3")]
        public double AllFact3 { get; set; }

        #endregion

        #region 4 chorak

        [Column("personal_budget_plan4")]
        public double PersonalBudgetPlan4 { get; set; }

        [Column("personal_budget_fact4")]
        public double PersonalBudgetFact4 { get; set; }

        [Column("local_investment_budget_plan4")]
        public double LocalInvestmentBudgetPlan4 { get; set; }

        [Column("local_investment_budget_fact4")]
        public double LocalInvestmentBudgetFact4 { get; set; }

        [Column("foreign_budget_plan4")]
        public double ForeignBudgetPlan4 { get; set; }

        [Column("foreign_budget_fact4")]
        public double ForeignBudgetFact4 { get; set; }

        [Column("other_budget_plan4")]
        public double OtherBudgetPlan4 { get; set; }

        [Column("other_budget_fact4")]
        public double OtherBudgetFact4 { get; set; }

        [Column("all_plan4")]
        public double AllPlan4 { get; set; }

        [Column("all_fact4")]
        public double AllFact4 { get; set; }
        #endregion
    }
}
