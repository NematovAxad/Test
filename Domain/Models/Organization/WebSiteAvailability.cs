using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Organization
{
    [Table("WebSiteAvailability", Schema = "organizations")]
    public class WebSiteAvailability : IDomain<int>
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
        [Column("successful_ping")]
        public int SuccessfulPing { get; set; }
        [Column("failed_ping")]
        public int FailedPing { get; set; }

    }
}
