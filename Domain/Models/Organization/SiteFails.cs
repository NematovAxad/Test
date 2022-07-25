using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Organization
{
    [Table("site_fails", Schema = "organizations")]
    public class SiteFails:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        [Column("website")]
        public string Website { get; set; }
        [Column("failed_time")]
        public DateTime FailedTime { get; set; }
    }
}
