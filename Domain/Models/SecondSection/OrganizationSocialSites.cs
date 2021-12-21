using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("org_social_sites", Schema = "organizations")]
    public class OrganizationSocialSites:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("field_id")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }
        public Field Field { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        public Deadline Deadline { get; set; }
        [Column("social_site_link")]
        public string SocialSiteLink { get; set; }
    }
}
