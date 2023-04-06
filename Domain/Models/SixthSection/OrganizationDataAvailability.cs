using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_data_availability", Schema = "organizations")]
    public class OrganizationDataAvailability:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        public Deadline Deadline { get; set; }

        [Column("section")]
        public string Section { get; set; }

        [Column("sphere")]
        public SpheresOrder Sphere { get; set; }

        [Column("data_availiability")]
        public bool DataAvailability { get; set; }

        [Column("data_relevance")]
        public bool DataRelevance { get; set; }

        [Column("set_date")]
        public DateTime SetDate { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("expert_pinfl")]
        public string ExpertPinfl { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }

    }
}
