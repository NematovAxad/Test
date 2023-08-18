using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_indicators", Schema = "organizations")]
    public class OrganizationIndicators:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get;set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("file_upload_date")]
        public DateTime FileUploadDate { get; set; }

        [Column("indicator_file_path")]
        public string IndicatorFilePath { get; set; }

        [Column("indicator_report_path")]
        public string IndicatorReportPath { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
