using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("organization_social_parameters", Schema = "organizations")]
    public class OrganizationSocialParameters : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        public Deadline Deadline { get; set; }
        [Column("org_full_name")]
        public bool? OrgFullName { get; set; }
        [Column("org_legal_site")]
        public bool? OrgLegalSite { get; set; }
        [Column("org_phone")]
        public bool? OrgPhone { get; set; }
        [Column("org_legal_address")]
        public bool? OrgLegalAddress { get; set; }
        [Column("org_email")]
        public bool? OrgEmail { get; set; }
        [Column("links_to_other_socials")]
        public bool? LinksToOtherSocials { get; set; }
        [Column("syncronized_posts")]
        public bool? SyncronizedPosts { get; set; }
        [Column("pool")]
        public bool? Pool { get; set; }
    }
}
