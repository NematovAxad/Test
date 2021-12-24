using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("helpline_info", Schema = "organizations")]
    public class HelplineInfo:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("helpline")]
        public string HelplineNumber { get; set; }
        [Column("official_site_has_helpline")]
        public bool OfficialSiteHasHelpline { get; set; }
        [Column("can_give_feedback_to_helpline")]
        public bool CanGiveFeedbackToHelpline { get; set; }
        [Column("official_site_has_helpline_feedback")]
        public bool OfficialSiteHasHelplinefeedback { get; set; }
    }
}
