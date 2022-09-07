using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("reestr_project_position", Schema = "organizations")]
    public class ReestrProjectPosition:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("reestr_project_id")]
        public int ReestrProjectId { get; set; }
        [Column("org_comment")]
        public string OrgComment { get; set; }
        [Column("screen_link")]
        public string ScreenLink { get; set; }
        [Column("expert_except")]
        public bool ExpertExcept { get; set; }
        [Column("expert_comment")]
        public string ExpertComment { get; set; }

    }
}
