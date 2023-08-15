using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("website_requirements", Schema = "organizations")]
    public class WebSiteRequirements:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("number")]
        public int Number { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("site_link_1")]
        public string SiteLink1 { get; set; }
        [Column("screen_link_1")]
        public string ScreenLink1 { get; set; }
        [Column("site_link_2")]
        public string SiteLink2 { get; set; }
        [Column("screen_link_2")]
        public string ScreenLink2 { get; set; }
        [Column("site_link_3")]
        public string SiteLink3 { get; set; }
        [Column("screen_link_3")]
        public string ScreenLink3 { get; set; }
        [Column("status")]
        public Steps RequirementStatus { get; set; }
        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
