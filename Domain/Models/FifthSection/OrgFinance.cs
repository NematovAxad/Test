using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("org_finance", Schema = "organizations")]
    public class OrgFinance:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("org_finance_amount")]
        public double OrgFinanceAmount { get; set; }
        [Column("org_it_finance_percent")]
        public double OrgItFinancePercent { get; set; }
        [Column("org_digitalization_finance_percent")]
        public double OrgDigitalizationFinancePercent { get; set; }

    }
}
