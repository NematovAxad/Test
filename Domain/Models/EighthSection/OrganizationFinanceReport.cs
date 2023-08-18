using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.EighthSection
{
    [Table("organization_finance_report", Schema = "organizations")]
    public class OrganizationFinanceReport : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("full_year_budget")]
        public double FullYearBudget { get; set; }

        [Column("full_year_spent_budget_percent")]
        public double FullYearSpentBudgetPercent { get; set; }

        [Column("full_year_digitalization_budget_percent")]
        public double FullYearDigitalizationBudgetPercent { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

    }
}
