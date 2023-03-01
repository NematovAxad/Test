using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("project_efficiency", Schema = "reestrprojects")]
    public class ProjectEfficiency:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectEfficiency ReestrProjectEfficiency { get; set; }

        [Column("efficiency_type")]
        public ProjectEfficiencyType EfficiencyType { get; set; }

        [Column("file_path")]
        public string FilePath { get; set; }

        [Column("org_comment")]
        public string OrgComment { get; set; }
    }
}
