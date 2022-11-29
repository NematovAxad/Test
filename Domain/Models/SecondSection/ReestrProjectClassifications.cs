﻿using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("reestr_project_classifications", Schema = "reestrprojects")]
    public class ReestrProjectClassifications:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }

        [Column("reestr_project_id")]
        public int ReestrProjectId { get; set; }

        [Column("exist")]
        public bool Exist { get; set; }

        [Column("org_comment")]
        public string OrgComment { get; set; }

        public ICollection<ProjectClassifications> Classifications { get; set; }

        [Column("expert_except")]
        public bool ExpertExcept { get; set; }
        [Column("expert_comment")]
        public string ExpertComment { get; set; }
    }
}