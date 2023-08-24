using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_authorizations", Schema = "reestrprojects")]
    public class ReestrProjectAuthorizations : IDomain<int>
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

        public ICollection<ProjectAuthorizations> Authorizations { get; set; }

        [Column("all_items")]
        public int AllItems { get; set; }

        [Column("excepted_items")]
        public int ExceptedItems { get; set; }
        [Column("expert_comment")]
        public string ExpertComment { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
    }
}
