using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("organization_apps", Schema = "organizations")]
    public class OrganizationApps:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("has_android_app")]
        public bool HasAndroidApp { get; set; }
        [Column("android_app_link")]
        public string AndroidAppLink { get; set; }
        [Column("has_ios_app")]
        public bool HasIosApp { get; set; }
        [Column("ios_app_link")]
        public string IosAppLink { get; set; }
        [Column("has_other_apps")]
        public bool HasOtherApps { get; set; }
        [Column("other_app_link")]
        public string OtherAppLink { get; set; }
        [Column("has_responsive_website")]
        public bool HasResponsiveWebsite { get; set; }
    }
}
