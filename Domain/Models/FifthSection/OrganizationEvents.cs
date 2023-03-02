using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("organization_events", Schema = "organizations")]
    public class OrganizationEvents:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("event_name")]
        public string EventName { get; set; }
        [Column("event_date")]
        public DateTime EventDate { get; set; }
    }
}
