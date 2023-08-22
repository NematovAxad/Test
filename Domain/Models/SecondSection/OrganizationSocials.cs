using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Models.SecondSection
{
    [Table("organization_socials", Schema = "organizations")]
    public class OrganizationSocials : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("messenger_link")]
        public string MessengerLink { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        [Column("link1")]
        public string Link1 { get; set; }
        [Column("post1")]
        public bool Post1 { get; set; }
        [Column("post1_link")]
        public string Post1Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("link2")]
        public string Link2 { get; set; }
        [Column("post2")]
        public bool Post2 { get; set; }
        [Column("post2_link")]
        public string Post2Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("link3")]
        public string Link3 { get; set; }
        [Column("post3")]
        public bool Post3 { get; set; }
        [Column("post3_link")]
        public string Post3Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("link4")]
        public string Link4 { get; set; }
        [Column("post4")]
        public bool Post4 { get; set; }
        [Column("post4_link")]
        public string Post4Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("link5")]
        public string Link5 { get; set; }
        [Column("post5")]
        public bool Post5 { get; set; }
        [Column("post5_link")]
        public string Post5Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("pool")]
        public bool? Pool { get; set; }
        [Column("pool_link")]
        public string PoolLink { get; set; }
        [Column("pool_screenshot_link")]
        public string PoolScreenshotLink { get; set; }
        [Column("pool_comment")]
        public string PoolComment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("pool_expert")]
        public bool? PoolExpert { get; set; }
      
        [Column("pool_comment_expert")]
        public string PoolCommentExpert { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("is_main")]
        public bool IsMain { get; set; }
        [Column("verified")]
        public bool? Verified {get;set;}
        [Column("syncronized_posts")]
        public bool? SyncronizedPosts { get; set; }
        [Column("comment_to_social_site")]
        public string CommentToSocialSite { get; set; }
        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
