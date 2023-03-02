using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("org_future_years_strategies", Schema = "organizations")]
    public class OrgFutureYearsStrategies:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("document_name")]
        public string DocumentName { get; set; }
        [Column("document_number")]
        public string DocumentNumber { get; set; }
        [Column("approval_date")]
        public DateTime ApprovalDate { get; set; }
        [Column("document_path")]
        public string DocumentPath { get; set; }
    }
}
