using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_exception", Schema = "reestrprojects")]
    public class ReestrProjectException:IDomain<int>
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
        public bool Exception { get; set; }

        [Column("expert_pinfl")]
        public string ExpertPinfl { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
    }
}
