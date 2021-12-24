using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.ThirdSection
{
    [Table("content_manager", Schema = "organizations")]
    public class OrganizationPublicServices:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("service_name")]
        public string ServiceName { get; set; }
        [Column("user_types")]
        public string UserTypes { get; set; }
        [Column("rendering_form")]
        public string RenderingForm { get; set; }
        [Column("portal_link")]
        public string PortalLink { get; set; }
        [Column("service_link")]
        public string ServiceLink { get; set; }
        [Column("mobile_app")]
        public string MobileApp { get; set; }
        [Column("other_resources")]
        public string OtherResources { get; set; }
        [Column("is_paid")]
        public bool IsPaid { get; set; }
        [Column("service_result")]
        public string ServiceResult { get; set; }
        [Column("service_other_result")]
        public string ServiceOtherResult { get; set; }
        [Column("mechanizm_for_tracking_progress")]
        public bool MechanizmForTrackingProgress { get; set; }
        [Column("tracking_progress_by")]
        public string TrackingProgressBy { get; set; }
        [Column("reglament_path")]
        public string ReglamentPath { get; set; }
        [Column("reglament_updated")]
        public bool ReglamentUpdated { get; set; }
    }
}
