using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("indicator_rating", Schema = "organizations")]
    public class IndicatorRating:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("all_indicators")]
        public int AllIndicators { get; set; }

        [Column("complete_indicators")]
        public int CompleteIndicators { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
