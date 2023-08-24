using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_position", Schema = "reestrprojects")]
    public class ReestrProjectPosition : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organizations { get; set; }

        [Column("reestr_project_id")]
        public int ReestrProjectId { get; set; }

        [Column("project_status")]
        public ReestrProjectStatusInNis ProjectStatus { get; set; }

        [Column("screen_link")]
        public string FilePath { get; set; }

        [Column("expert_except")]
        public bool ExpertExcept { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

    }
}
